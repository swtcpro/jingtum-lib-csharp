using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    internal class RequestCache
    {
        public string Command { get; set; }
        public object Data { get; set; }
        public Func<object, object> Filter { get; set; }
        public MessageCallback<object> Callback { get; set; }
    }

    internal class PathCache
    {
        public string Path { get; set; }
        public object Choice { get; set; }
    }
}
