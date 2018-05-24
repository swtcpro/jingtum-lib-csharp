using JingTum.Lib;
using System;
using System.ComponentModel;

namespace TestApp
{
    class TxSettings
    {
        [Category("Required")]
        public string Secret { get; set; }
        [Category("Optional")]
        public string Memo { get; set; }
        [Category("Optional")]
        public UInt32? Fee { get; set; }
        [Category("Optional")]
        public string Path { get; set; }
        [Category("Optional")]
        public AmountSettings SendMax { get; set; }
        [Category("Optional")]
        public float? TransferRate { get; set; }
        [Category("Optional")]
        public UInt32? Flags { get; set; }
    }
}
