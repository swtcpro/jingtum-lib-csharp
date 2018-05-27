using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Api
{
    internal static class Config
    {
        public const int Timeout = 1000 * 60;
        public const string Currency = "SWT";
        public const string AmountFormat = "0.######";
        public const double FreezedReserved = 20.0;
        public const double FreezedEachFreezed = 5.0;
    }
}
