using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    /// <summary>
    /// Represents the errors from jingtum system.
    /// </summary>
    public static class Errors
    {
        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <param name="errorKey">The error key.</param>
        /// <returns>The error message string. null if the key is not found.</returns>
        public static string GetErrorMessage(string errorKey)
        {
            if (errorKey == null) return null;

            var key = SimplifyName(errorKey);
            foreach(var pair in ErrorMessages)
            {
                if(key == SimplifyName(pair.Key))
                {
                    return pair.Value;
                }
            }

            return null;
        }

        private static string SimplifyName(string name)
        {
            return name.ToUpper().Replace("_", "");
        }

        private static IDictionary<string, string> _errors;

        /// <summary>
        /// Gets the dictionary of the error messages.
        /// </summary>
        public static IDictionary<string, string> ErrorMessages
        {
            get
            {
                if (_errors == null)
                {
                    var list = new Dictionary<string, string>();
                    foreach (var f in typeof(Errors).GetFields(BindingFlags.Static | BindingFlags.NonPublic))
                    {
                        if(f.FieldType == typeof(string) && f.Name.StartsWith("tec"))
                        {
                            list.Add(f.Name, (string)f.GetValue(null));
                        }
                    }

                    _errors = list;
                }

                return _errors;
            }
        }

        private const string tecCLAIM = "Fee claimed. Sequence used. No action.";
        private const string tecDIR_FULL = "Can not add entry to full directory";
        private const string tecFAILED_PROCESSING = "Failed to correctly process transaction.";
        private const string tecINSUF_RESERVE_LINE = "Insufficient reserve to add trust line.";
        private const string tecINSUF_RESERVE_OFFER = "Insufficient reserve to create offer.";
        private const string tecNO_DST = "Destination does not exist. Send SWT to create it.";
        private const string tecNO_DST_INSUF_SWT = "Destination does not exist. Too little SWT sent to create it";
        private const string tecNO_LINE_INSUF_RESERVE = "No such line. Too little reserve to create it.";
        private const string tecNO_LINE_REDUNDANT = "Can't set non-existent line to default.";
        private const string tecPATH_DRY = "Path could not send partial amount.";
        private const string tecPATH_PARTIAL = "Path could not send full amount.";
        private const string tecMASTER_DISABLED = "Master key is disabled.";
        private const string tecNO_REGULAR_KEY = "Regular key is not set.";
        private const string tecUNFUNDED = "One of _ADD, _OFFER, or _SEND. Deprecated.";
        private const string tecUNFUNDED_ADD = "Insufficient SWT balance for WalletAdd.";
        private const string tecUNFUNDED_OFFER = "Insufficient balance to fund created offer";
        private const string tecUNFUNDED_PAYMENT = "Insufficient SWT balance to send.";
        private const string tecOWNERS = "Non-zero owner count.";
        private const string tecNO_ISSUER = "Issuer account does not exist.";
        private const string tecNO_AUTH = "Not authorized to hold asset.";
        private const string tecNO_LINE = "No such line.";
        private const string tecINSUFF_FEE = "Insufficient balance to pay fee.";
        private const string tecFROZEN = "Asset is frozen.";
        private const string tecNO_TARGET = "Target account does not exist.";
        private const string tecNO_PERMISSION = "No permission to perform requested operation.";
        private const string tecNO_ENTRY = "No matching entry found.";
        private const string tecINSUFFICIENT_RESERVE = "Insufficient reserve to complete requested operation";
        private const string tecNEED_MASTER_KEY = "The operation requires the use of the Master Key";
        private const string tecDST_TAG_NEEDED = "A destination tag is required.";
        private const string tecINTERNAL = "An internal error has occurred during processing.";
        private const string tefALREADY = "The exact transaction was already in this ledger.";
        private const string tefBAD_ADD_AUTH = "Not authorized to add account.";
        private const string tefBAD_AUTH = "Transaction's private key is not authorized.";
        private const string tefBAD_LEDGER = "Ledger in unexpected state.";
        private const string tefCREATED = "Can't add an already created account.";
        private const string tefEXCEPTION = "Unexpected program state.";
        private const string tefFAILURE = "Failed to apply";
        private const string tefINTERNAL = "Internal error.";
        private const string tefMASTER_DISABLED = "Master key is disabled.";
        private const string tefMAX_LEDGER = "Ledger sequence too high.";
        private const string tefNO_AUTH_REQUIRED = "Auth is not required.";
        private const string tefPAST_SEQ = "This sequence number has already past.";
        private const string tefWRONG_PRIOR = "This previous transaction does not match.";
        private const string telLOCAL_ERROR = "Local failure.";
        private const string telBAD_DOMAIN = "Domain too long.";
        private const string telBAD_PATH_COUNT = "Malformed: Too many paths";
        private const string telBAD_private_KEY = "private key too long.";
        private const string telFAILED_PROCESSING = "Failed to correctly process transaction.";
        private const string telINSUF_FEE_P = "Fee insufficient.";
        private const string telNO_DST_PARTIAL = "Partial payment to create account not allowed.";
        private const string telBLKLIST = "Tx disable for blacklist.";
        private const string telINSUF_FUND = "Fund insufficient.";
        private const string temMALFORMED = "Malformed transaction.";
        private const string temBAD_AMOUNT = "Can only send positive amounts.";
        private const string temBAD_AUTH_MASTER = "Auth for unclaimed account needs correct master key";
        private const string temBAD_CURRENCY = "Malformed: Bad currency.";
        private const string temBAD_EXPIRATION = "Malformed: Bad expiration.";
        private const string temBAD_FEE = "Invalid fee, negative or not SWT";
        private const string temBAD_ISSUER = "Malformed: Bad issuer";
        private const string temBAD_LIMIT = "Limits must be non-negative.";
        private const string temBAD_QUORUM = "Quorums must be non-negative.";
        private const string temBAD_WEIGHT = "Weights must be non-negative";
        private const string temBAD_OFFER = "Malformed: Bad offer";
        private const string temBAD_PATH = "Malformed: Bad path.";
        private const string temBAD_PATH_LOOP = "Malformed: Loop in path.";
        private const string temBAD_SEND_SWT_LIMIT = "Malformed: Limit quality is not allowed for SWT to SWT";
        private const string temBAD_SEND_SWT_MAX = "Malformed: Send max is not allowed for SWT to SWT.";
        private const string temBAD_SEND_SWT_NO_DIRECT = "Malformed: No Skywell direct is not allowed for SWT to SWT.";
        private const string temBAD_SEND_SWT_PARTIAL = "Malformed: Partial payment is not allowed for SWT to SWT.";
        private const string temBAD_SEND_SWT_PATHS = "Malformed: Paths are not allowed for SWT to SWT";
        private const string temBAD_SEQUENCE = "Malformed: Sequence is not in the past.";
        private const string temBAD_SIGNATURE = "Malformed: Bad signature.";
        private const string temBAD_SRC_ACCOUNT = "Malformed: Bad source account.";
        private const string temBAD_TRANSFER_RATE = "Malformed: Transfer rate must be >= 1.0";
        private const string temDST_IS_SRC = "Destination may not be source.";
        private const string temDST_NEEDED = "Destination not specified.";
        private const string temINVALID = "The transaction is ill-formed.";
        private const string temINVALID_FLAG = "The transaction has an invalid flag.";
        private const string temREDUNDANT = "Sends same currency to self.";
        private const string temREDUNDANTSIGN = "Add self as additional sign.";
        private const string temSKYWELL_EMPTY = "PathSet with no paths.";
        private const string temUNCERTAIN = "In process of determining result. Never returned.";
        private const string temUNKNOWN = "The transaction requires logic that is not implemented yet.";
        private const string temDISABLED = "The transaction requires logic that is currently disabled.";
        private const string temMULTIINIT = "contract code has multi init function";
        private const string terRETRY = "Retry transaction.";
        private const string terFUNDS_SPENT = "Can't set password, password set funds already spent";
        private const string terINSUF_FEE_B = "Account balance can't pay fee.";
        private const string terLAST = "Process last";
        private const string terNO_SKYWELL = "Path does not permit rippling.";
        private const string terNO_ACCOUNT = "The source account does not exist.";
        private const string terNO_AUTH = "Not authorized to hold IOUs.";
        private const string terNO_LINE = "No such line.";
        private const string terPRE_SEQ = "Missing/inapplicable prior transaction.";
        private const string terOWNERS = "Non-zero owner count.";
        private const string tesSUCCESS = "The transaction was applied. Only final in a validated ledger";


    }
}
