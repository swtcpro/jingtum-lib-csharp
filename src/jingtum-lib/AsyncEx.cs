using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    internal static class AsyncEx
    {
        public static Task Complete()
        {
            return Task.FromResult(0);
        }
    }
}
