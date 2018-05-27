using JingTum.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Api
{
    public class Balances
    {
        public static Task GetBalance(Remote remote, Options options, MessageCallback<Response> callback = null)
        {
            Func<Exception, Task> onException = (ex =>
            {
                callback(new MessageResult<Response>(null, ex));
                return Task.FromResult(0);
            });

            if (remote == null || !remote.IsConnected) return onException(new ArgumentException("Remote is disconnected."));
            if (options == null) return onException(new ArgumentNullException("options"));
            if (!Utils.IsValidAddress(options.Account)) return onException(new Exception("Invalid account address."));

            var condition = new Amount();
            if (options.Currency != null)
            {
                if (!Utils.IsValidCurrency(options.Currency))
                {
                    return onException(new Exception("Invalid currency."));
                }

                condition.Currency = options.Currency;
            }

            if (options.Issuer != null)
            {
                if (!Utils.IsValidAddress(options.Issuer))
                {
                    return onException(new Exception("Invalid issuer address."));
                }

                condition.Issuer = options.Issuer;
            }

            var results = new Results();

            var opt1 = new AccountInfoOptions();
            opt1.Account = options.Account;
            var t1 = remote.RequestAccountInfo(opt1).SubmitAsync(r1 =>
            {
                if (r1.Exception != null) results.Exception = r1.Exception;
                results.Native = r1.Result;
            });

            var opt2 = new AccountRelationsOptions();
            opt2.Account = options.Account;
            opt2.Type = RelationType.Trust;
            var t2 = remote.RequestAccountRelations(opt2).SubmitAsync(r2 =>
            {
                if (r2.Exception != null) results.Exception = r2.Exception;
                results.Lines = r2.Result;
            });

            var opt3 = new AccountRelationsOptions();
            opt3.Account = options.Account;
            opt3.Type = RelationType.Freeze;
            var t3 = remote.RequestAccountRelations(opt3).SubmitAsync(r3 =>
            {
                if (r3.Exception != null) results.Exception = r3.Exception;
                results.Lines2 = r3.Result;
            });

            var opt4 = new AccountOffersOptions();
            opt4.Account = options.Account;

            Func<Task> getOffers = null;
            getOffers = () =>
            {
                var t4 = remote.RequestAccountOffers(opt4).SubmitAsync(r4 =>
                {
                    if (r4.Exception != null)
                        results.Exception = r4.Exception;
                    else
                    {
                        if (r4.Result.Offers != null)
                        {
                            results.Offers = r4.Result.Offers.Concat(results.Offers ?? new Offer[0]).ToArray();
                        }

                        if (r4.Result.Marker != null)
                        {
                            opt4.Marker = r4.Result.Marker;
                            var childTask = getOffers();
                            childTask.Wait();
                        }
                    }
                });

                return t4;
            };

            var t5 = getOffers();

            var task = new Task(() =>
            {
                if (!Task.WaitAll(new Task[] { t1, t2, t3, t5 }, Config.Timeout))
                {
                    onException(new Exception("Time out."));
                    return;
                }

                if (results.Exception != null)
                {
                    onException(results.Exception);
                    return;
                }

                var balances = ProcessBalance(results, condition);
                var response = new Response { Balances=balances, Sequence=results.Native.AccountData.Sequence };
                callback(new MessageResult<Response>(null, null, response));
                return;
            });

            task.Start();
            return task;
        }

        private static Balance[] ProcessBalance(Results data, Amount condition)
        {
            var swtValue = data.Native.AccountData.Balance.Value;
            var freezedCount = (data.Lines.Lines == null ? 0 : data.Lines.Lines.Length) + data.Offers.Length;
            var freeze0 = Config.FreezedReserved + freezedCount * Config.FreezedEachFreezed;
            var swt = new Balance { Currency = Config.Currency, Issuer = "", Value = swtValue, Freezed = freeze0.ToString(Config.AmountFormat) };

            var balances = new List<Balance>();
            if((condition.Currency==null && condition.Issuer==null) || condition.Currency == Config.Currency){
                balances.Add(swt);
            }

            if (condition.Currency == Config.Currency)
            {
                return balances.ToArray();
            }

            foreach (var trustLine in data.Lines.Lines)
            {
                var currency = trustLine.Currency;
                var issuer = trustLine.Issuer ?? trustLine.Account;

                if ((condition.Currency != null && condition.Currency != currency)
                    || (condition.Issuer != null && condition.Issuer != issuer))
                {
                    continue;
                }

                var balance = new Balance { Currency = currency, Issuer = issuer, Value = trustLine.Balance };
                var freezed = 0d;
                foreach (var offer in data.Offers)
                {
                    var takerGets = offer.TakerGets;
                    if (takerGets.Currency == swt.Currency && takerGets.Issuer == swt.Issuer)
                    {
                        var tmpFreezed = double.Parse(swt.Freezed) + double.Parse(takerGets.Value);
                        swt.Freezed = tmpFreezed.ToString();
                    }
                    else if (takerGets.Currency == balance.Currency && takerGets.Issuer == balance.Issuer)
                    {
                        freezed += double.Parse(takerGets.Value);
                    }
                }

                foreach (var freezeLine in data.Lines2.Lines)
                {
                    if (freezeLine.Currency == balance.Currency && freezeLine.Issuer == balance.Issuer)
                    {
                        freezed += double.Parse(freezeLine.Limit);
                    }
                }

                balance.Freezed = freezed.ToString("0.######");
                balances.Add(balance);
            }

            return balances.ToArray();
        }

        private class Results
        {
            public Results()
            {
                Offers = new Offer[0];
            }

            public Exception Exception { get; set; }
            public AccountInfoResponse Native { get; set; }
            public AccountRelationsResponse Lines { get; set; }
            public AccountRelationsResponse Lines2 { get; set; }
            public Offer[] Offers { get; set; }
        }

        public class Options
        {
            public string Account { get; set; }
            public string Currency { get; set; }
            public string Issuer { get; set; }
        }

        public class Response
        {
            public Balance[] Balances { get; internal set; }
            public UInt32 Sequence { get; internal set; }
        }

        public class Balance : Amount
        {
            public string Freezed { get; internal set; }
        }
    }
}
