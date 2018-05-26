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
        public Task GetBalance(Remote remote, Options options, MessageCallback<Response> callback = null)
        {
            Action<Exception> onException = (ex =>
            {
                callback(new MessageResult<Response>(null, ex));
            });

            if (remote == null || !remote.IsConnected) onException(new ArgumentException("Remote is disconnected."));
            if (options == null) onException(new ArgumentNullException("options"));
            if (!Utils.IsValidAddress(options.Account)) onException(new Exception("Invalid account address."));

            var condition = new Amount();
            if (options.Currency != null)
            {
                if (!Utils.IsValidCurrency(options.Currency))
                {
                    onException(new Exception("Invalid currency."));
                }

                condition.Currency = options.Currency;
            }

            if (options.Issuer != null)
            {
                if (!Utils.IsValidAddress(options.Issuer))
                {
                    onException(new Exception("Invalid issuer address."));
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
                var task = remote.RequestAccountOffers(opt4).SubmitAsync(r4 =>
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

                return task;
            };

            var t4 = getOffers();

            var t = new Task(() =>
            {
                if (!Task.WaitAll(new Task[] { t1, t2, t3, t4 }, 1000 * 60))
                {
                    onException(new Exception("Time out."));
                    return;
                }

                if (results.Exception != null)
                {
                    onException(results.Exception);
                    return;
                }

                var response = ProcessBalance(results, condition);
                callback(new MessageResult<Response>(null, null, response));
                return;
            });
            t.Start();
            return t;
        }

        private Response ProcessBalance(Results results, Amount condition = null)
        {
            return null;
        }

        private class Results
        {
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
            public string Freeze { get; internal set; }
        }
    }
}
