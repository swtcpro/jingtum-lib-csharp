using JingTum.Lib;
using System;
using System.ComponentModel;

namespace TestApp
{
    class TxSettings
    {
        public string Secret { get; set; }
        public string Memo { get; set; }
        public UInt32? Fee { get; set; }
        public string Path { get; set; }
        public Amount SendMax { get; set; }
        public float? TransferRate { get; set; }
        public UInt32? Flags { get; set; }
    }
}
