using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JingTum.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    /// <summary>
    /// Unit tests compare with the result of Node.js.
    /// </summary>
    [TestClass]
    public class SerializerTests
    {
        [TestMethod]
        [DataRow((byte)0, "00")]
        [DataRow((byte)1, "01")]
        [DataRow((byte)10, "0A")]
        [DataRow((byte)100, "64")]
        [DataRow((byte)128, "80")]
        [DataRow(byte.MaxValue, "FF")]
        public void TestSerializedInt8(byte val, string hex)
        {
            var so = new Serializer();
            var st = new SerializedInt8();
            st.Serialize(so, val);
            Assert.AreEqual(hex, so.ToHex());

            so = new Serializer();
            var sti = st as ISerializedType;
            sti.Serialize(so, val);
            Assert.AreEqual(hex, so.ToHex());
        }

        [TestMethod]
        [DataRow((ushort)0, "0000")]
        [DataRow((ushort)100, "0064")]
        [DataRow((ushort)255, "00FF")]
        [DataRow((ushort)1000, "03E8")]
        [DataRow((ushort)32767, "7FFF")]
        [DataRow(ushort.MaxValue, "FFFF")]
        public void TestSerializedInt16(ushort val, string hex)
        {
            var so = new Serializer();
            var st = new SerializedInt16();
            st.Serialize(so, val);
            Assert.AreEqual(hex, so.ToHex());

            so = new Serializer();
            var sti = st as ISerializedType;
            sti.Serialize(so, val);
            Assert.AreEqual(hex, so.ToHex());
        }

        [TestMethod]
        [DataRow((uint)0, "00000000")]
        [DataRow((uint)100, "00000064")]
        [DataRow((uint)255, "000000FF")]
        [DataRow((uint)1000, "000003E8")]
        [DataRow((uint)32767, "00007FFF")]
        [DataRow((uint)1000000, "000F4240")]
        [DataRow((uint)2147483647, "7FFFFFFF")]
        [DataRow(uint.MaxValue, "FFFFFFFF")]
        public void TestSerializedInt32(uint val, string hex)
        {
            var so = new Serializer();
            var st = new SerializedInt32();
            st.Serialize(so, val);
            Assert.AreEqual(hex, so.ToHex());

            so = new Serializer();
            var sti = st as ISerializedType;
            sti.Serialize(so, val);
            Assert.AreEqual(hex, so.ToHex());
        }

        [TestMethod]
        [DataRow((ulong)0, "0000000000000000")]
        [DataRow((uint)1000, "00000000000003E8")]
        [DataRow((ulong)2147483647, "000000007FFFFFFF")]
        [DataRow((ulong)0x20000000000000 - 1, "001FFFFFFFFFFFFF")]
        [DataRow(ulong.MaxValue, "FFFFFFFFFFFFFFFF")]
        public void TestSerializedInt64(ulong val, string hex)
        {
            var so = new Serializer();
            var st = new SerializedInt64();
            st.Serialize(so, val);
            Assert.AreEqual(hex, so.ToHex());

            so = new Serializer();
            var sti = st as ISerializedType;
            sti.Serialize(so, val);
            Assert.AreEqual(hex, so.ToHex());
        }

        [TestMethod]
        [DataRow((byte)1, "0000000000000001")]
        [DataRow(10, "000000000000000A")]
        [DataRow(100L, "0000000000000064")]
        [DataRow((ulong)1000, "00000000000003E8")]
        [DataRow("10", "0000000000000010")] // string means hex
        [DataRow("E8", "00000000000000E8")]
        [DataRow("7FE8", "0000000000007FE8")]
        [DataRow("1F2E4D5C6B7A8F9E", "1F2E4D5C6B7A8F9E")]
        [DataRow("FFFFFFFFFFFFFFFF", "FFFFFFFFFFFFFFFF")]
        public void TestSerializedIn64_Object(object val, string hex)
        {
            var so = new Serializer();
            var sti = new SerializedInt64() as ISerializedType;
            sti.Serialize(so, val);
            Assert.AreEqual(hex, so.ToHex());
        }

        [TestMethod]
        [DataRow("8E", "8E")]
        [DataRow("E", "0E")]
        [DataRow("1F2E4D5C6B7A8F9E", "1F2E4D5C6B7A8F9E")]
        [DataRow("1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E", "1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E")]
        public void TestSerializedHash128(string val, string hex)
        {
            var so = new Serializer();
            var st = new SerializedHash128();
            st.Serialize(so, val);
            Assert.AreEqual(hex, so.ToHex());

            so = new Serializer();
            var sti = st as ISerializedType;
            sti.Serialize(so, val);
            Assert.AreEqual(hex, so.ToHex());
        }

        [TestMethod]
        [DataRow("8E", "8E")]
        [DataRow("E", "0E")]
        [DataRow("1F2E4D5C6B7A8F9E", "1F2E4D5C6B7A8F9E")]
        [DataRow("1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E", "1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E")]
        [DataRow("1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E", "1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E")]
        public void TestSerializedHash256(string val, string hex)
        {
            var so = new Serializer();
            var st = new SerializedHash256();
            st.Serialize(so, val);
            Assert.AreEqual(hex, so.ToHex());

            so = new Serializer();
            var sti = st as ISerializedType;
            sti.Serialize(so, val);
            Assert.AreEqual(hex, so.ToHex());
        }

        [TestMethod]
        public void TestSerializedHash160()
        {
            // maybe the Node.js version is incorrect
            throw new NotImplementedException();
        }

        [TestMethod]
        [DataRow("CNY", "000000000000000000000000434E590000000000")]
        [DataRow("USD", "0000000000000000000000005553440000000000")]
        [DataRow("SWT", "0000000000000000000000005357540000000000")]
        [DataRow("800000000000000000000000A95EFD7EC3101635", "800000000000000000000000A95EFD7EC3101635")]
        [DataRow("7EC31016357EC3101635A95EFD7EC3A95EFD7EC3", "7EC31016357EC3101635A95EFD7EC3A95EFD7EC3")]
        public void TestSerializedCurrency(string val, string hex)
        {
            var so = new Serializer();
            var st = new SerializedCurrency();
            st.Serialize(so, val);
            Assert.AreEqual(hex, so.ToHex());
        }

        [TestMethod]
        [DataRow(123.123, "400000000756B538")]
        [DataRow("0.5", "400000000007A120")]
        [DataRow("100", "4000000005F5E100")]
        [DataRow("0.001", "40000000000003E8")]
        [DataRow("200000", "4000002E90EDD000")]
        [DataRow("212345678", "4000C12094B4EF80")]
        public void TestSerializedAmount_SWT(object val, string hex)
        {
            var so = new Serializer();
            var st = new SerializedAmount() as ISerializedType;
            st.Serialize(so, val);
            Assert.AreEqual(hex, so.ToHex());
        }

        [TestMethod]
        [DataRow("SWT", "", "129757.754575", "4000001E362A34CF")]
        [DataRow("CNY", "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", "129757.7", "D5C49C23B0275A00000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D41596")]
        [DataRow("USD", "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1", "7.754575", "D49B8CBEB0425600000000000000000000000000555344000000000060B1227191135B3B16CB1D74F2509BD5C5DF985B")]
        public void TestSerializedAmount(string currenty, string issuer, string value, string hex)
        {
            var so = new Serializer();
            var st = new SerializedAmount() as ISerializedType;
            st.Serialize(so, new AmountSettings { Currency = currenty, Issuer = issuer, Value = value });
            Assert.AreEqual(hex, so.ToHex());
        }

        [TestMethod]
        [DataRow("4000C12094B4EF80", "084000C12094B4EF80")]
        [DataRow("4EC57F3CD2D415964EC57F3CD2D41596", "104EC57F3CD2D415964EC57F3CD2D41596")]
        [DataRow("4000C12094B4EF804EC57F3CD2D415964EC57F3CD2D415964000C12094B4EF", "1F4000C12094B4EF804EC57F3CD2D415964EC57F3CD2D415964000C12094B4EF")]
        public void TestSerializedVariableLength(string val, string hex)
        {
            var so = new Serializer();
            var st = new SerializedVariableLength() as ISerializedType;
            st.Serialize(so, val);
            Assert.AreEqual(hex, so.ToHex());
        }

        [TestMethod]
        [DataRow("jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr", "14DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47")]
        [DataRow("j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1", "1460B1227191135B3B16CB1D74F2509BD5C5DF985B")]
        [DataRow("jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", "14A582E432BFC48EEDEF852C814EC57F3CD2D41596")]
        public void TestSerializedAccount(string val, string hex)
        {
            var so = new Serializer();
            var st = new SerializedAccount() as ISerializedType;
            st.Serialize(so, val);
            Assert.AreEqual(hex, so.ToHex());
        }

        [TestMethod]
        public void TestSerializedPathSet()
        {
            var pathSet1 = new PathComputed[][] {new[] { new PathComputed {Currency="CNY", Issuer= "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", Type=48 } } };
            var pathSet2 = new PathComputed[][] { new[] { new PathComputed {Account="jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", Currency = "CNY", Issuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", Type = 49 } } };
            var pathSet3 = new PathComputed[][] { new[] { new PathComputed { Currency = "CNY", Issuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or" }, new PathComputed { Currency= "USD", Issuer= "jEoSyfChhUMzpRDttAJXuie8XhqyoPBYvV" } } };
            var pathSet4 = new PathComputed[][] { new[] { new PathComputed { Currency = "CNY", Issuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or" } }, new[] { new PathComputed { Currency = "USD", Issuer = "jEoSyfChhUMzpRDttAJXuie8XhqyoPBYvV" } } };
            var pathSet5 = new PathComputed[][] { new[] { new PathComputed { Currency = "CNY", Issuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or" } }, new[] { new PathComputed { Currency = "USD", Issuer = "jEoSyfChhUMzpRDttAJXuie8XhqyoPBYvV" } }, new PathComputed[0] };
            var pathSet6 = new PathComputed[][] { new PathComputed[] { }};
            var pathSet7 = new PathComputed[][] { new PathComputed[] { }, new PathComputed[] { } };

            TestSerializedPathSetImp("PathSet1", pathSet1, "30000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D4159600");
            TestSerializedPathSetImp("PathSet2", pathSet2, "31A582E432BFC48EEDEF852C814EC57F3CD2D41596000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D4159600");
            TestSerializedPathSetImp("PathSet3", pathSet3, "30000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D41596300000000000000000000000005553440000000000A25A9260C579E8FA76B95B4BA7334C406DF7C65900");
            TestSerializedPathSetImp("PathSet4", pathSet4, "30000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D41596FF300000000000000000000000005553440000000000A25A9260C579E8FA76B95B4BA7334C406DF7C65900");
            TestSerializedPathSetImp("PathSet5", pathSet5, "30000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D41596FF300000000000000000000000005553440000000000A25A9260C579E8FA76B95B4BA7334C406DF7C659FF00");
            TestSerializedPathSetImp("PathSet6", pathSet6, "00");
            TestSerializedPathSetImp("PathSet7", pathSet7, "FF00");
        }

        private void TestSerializedPathSetImp(string name, PathComputed[][] val, string hex)
        {
            var so = new Serializer();
            var st = new SerializedPathSet();
            st.Serialize(so, val);
            Assert.AreEqual(hex, so.ToHex(), false, name);
        }

        [TestMethod]
        [DataRow(new string[0], "00")]
        [DataRow(new[] { "1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E" }, "201F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E")]
        [DataRow(new[] { "1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E", "1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E" }, "401F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E")]
        [DataRow(new[] { "1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E", "1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E", "1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E" }, "601F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E1F2E4D5C6B7A8F9E")]
        public void TestSerializedVector256(string[] val, string hex)
        {
            var so = new Serializer();
            var st = new SerializedVector256();
            st.Serialize(so, val);
            Assert.AreEqual(hex, so.ToHex());
        }

        [TestMethod]
        [DataRow("", "7D0100E1")]
        [DataRow("http://www.jingtum.com", "7D16687474703A2F2F7777772E6A696E6774756D2E636F6DE1")]
        [DataRow("I Love SWTC!", "7D0C49204C6F7665205357544321E1")]
        [DataRow("我就是神。", "7D0FE68891E5B0B1E698AFE7A59EE38082E1")]
        [DataRow("中English混合。", "7D13E4B8AD456E676C697368E6B7B7E59088E38082E1")]
        [DataRow("：;/?:@&=+$,# - _.!~*'()", "7D19EFBC9A3B2F3F3A40263D2B242C23202D205F2E217E2A272829E1")]
        public void TestSerializedMemo(string memo, string hex)
        {
            var so = new Serializer();
            var st = new SerializedMemo();
            var val = new MemoDataInfo { MemoData = memo };
            st.Serialize(so, val);
            Assert.AreEqual(hex, so.ToHex());
        }

        [TestMethod]
        [DataRow("!#$&'()*+,-./:;=?@_~1aA啊", "212324262728292A2B2C2D2E2F3A3B3D3F405F7E316141E5958A")]
        public void TestConvertStringToHex(string input, string hex)
        {
            var result = SerializedTypeHelper.ConvertStringToHex(input);
            Assert.AreEqual(hex, result);
        }

        [TestMethod]
        public void TestSerializePaymentTxData()
        {
            var data = new PaymentTxData();
            data.Flags = 0;
            data.Fee = (decimal)0.0001;
            data.SigningPubKey = "02A8D70000DCCDAE1E639E5938559B840FA865F5C5B31ADCCE76EF51A7F71039D5";
            data.Sequence = 9;
            data.TransactionType = TransactionType.Payment;

            data.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            data.Destination = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            data.Amount = "0.05";

            var so = Serializer.Create(data);
            Assert.AreEqual("1200002200000000240000000961400000000000C350684000000000000064732102A8D70000DCCDAE1E639E5938559B840FA865F5C5B31ADCCE76EF51A7F71039D58114DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47831460B1227191135B3B16CB1D74F2509BD5C5DF985B", so.ToHex());
        }

        [TestMethod]
        public void TestSerializePaymentTxData_CNY()
        {
            var data = new PaymentTxData();
            data.Flags = 0;
            data.Fee = (decimal)0.0001;
            data.SigningPubKey = "02A8D70000DCCDAE1E639E5938559B840FA865F5C5B31ADCCE76EF51A7F71039D5";
            data.Sequence = 9;
            data.TransactionType = TransactionType.Payment;

            data.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            data.Destination = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            data.Amount = new AmountSettings { Currency = "CNY", Issuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", Value = "12.33" };

            var so = Serializer.Create(data);
            Assert.AreEqual("1200002200000000240000000961D4C461682F021000000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D41596684000000000000064732102A8D70000DCCDAE1E639E5938559B840FA865F5C5B31ADCCE76EF51A7F71039D58114DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47831460B1227191135B3B16CB1D74F2509BD5C5DF985B", so.ToHex());
        }

        [TestMethod]
        public void TestSerializePaymentTxData_SendMax()
        {
            var data = new PaymentTxData();
            data.Flags = 0;
            data.Fee = (decimal)0.0001;
            data.SigningPubKey = "02A8D70000DCCDAE1E639E5938559B840FA865F5C5B31ADCCE76EF51A7F71039D5";
            data.Sequence = 9;
            data.TransactionType = TransactionType.Payment;

            data.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            data.Destination = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            data.Amount = "0.05";

            data.SendMax= new AmountSettings { Currency = "CNY", Issuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", Value = "12.33" };

            var so = Serializer.Create(data);
            Assert.AreEqual("1200002200000000240000000961400000000000C35068400000000000006469D4C461682F021000000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D41596732102A8D70000DCCDAE1E639E5938559B840FA865F5C5B31ADCCE76EF51A7F71039D58114DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47831460B1227191135B3B16CB1D74F2509BD5C5DF985B", so.ToHex());
        }

        [TestMethod]
        public void TestSerializePaymentTxData_PathSet()
        {
            var data = new PaymentTxData();
            data.Flags = 0;
            data.Fee = (decimal)0.0001;
            data.SigningPubKey = "02A8D70000DCCDAE1E639E5938559B840FA865F5C5B31ADCCE76EF51A7F71039D5";
            data.Sequence = 9;
            data.TransactionType = TransactionType.Payment;

            data.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            data.Destination = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            data.Amount = "0.05";

            data.Paths = new PathComputed[][]{ new PathComputed[] { new PathComputed { Currency="CNY", Issuer= "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or" } } };

            var so = Serializer.Create(data);
            Assert.AreEqual("1200002200000000240000000961400000000000C350684000000000000064732102A8D70000DCCDAE1E639E5938559B840FA865F5C5B31ADCCE76EF51A7F71039D58114DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47831460B1227191135B3B16CB1D74F2509BD5C5DF985B011230000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D4159600", so.ToHex());
        }

        [TestMethod]
        public void TestSerializePaymentTxData_Full()
        {
            var data = new PaymentTxData();
            data.Flags = 0;
            data.Fee = (decimal)0.0001;
            data.SigningPubKey = "02A8D70000DCCDAE1E639E5938559B840FA865F5C5B31ADCCE76EF51A7F71039D5";
            data.Sequence = 9;
            data.TransactionType = TransactionType.Payment;

            data.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            data.Destination = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            data.Amount = "0.05";

            data.Fee = (decimal)0.001;
            data.Flags = 1;
            data.SendMax = 1000000;
            data.TransferRate = 1500000000;
            data.Memos = new List<MemoInfo>() { new MemoInfo { Memo = new MemoDataInfo { MemoData = "I Love SWTC." } } };

            var so = Serializer.Create(data);
            Assert.AreEqual("120000220000000124000000092B59682F0061400000000000C3506840000000000003E869400000E8D4A51000732102A8D70000DCCDAE1E639E5938559B840FA865F5C5B31ADCCE76EF51A7F71039D58114DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47831460B1227191135B3B16CB1D74F2509BD5C5DF985BF9EA7D0C49204C6F766520535754432EE1F1", so.ToHex());
        }

        [TestMethod]
        public void TestSerializePaymentTxData_TxnSignature()
        {
            var data = new PaymentTxData();
            data.Flags = 0;
            data.Fee = (decimal)0.01;
            data.SigningPubKey = "0204B7DE11FDC08FBBC007000BAD727E3F472DCB7BAC7078A69EFB748F242CF6EE";
            data.Sequence = 35;
            data.TransactionType = TransactionType.Payment;

            data.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            data.Destination = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            data.Amount = "0.5";

            data.TxnSignature = "3045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB";

            var so = Serializer.Create(data);
            Assert.AreEqual("1200002200000000240000002361400000000007A12068400000000000271073210204B7DE11FDC08FBBC007000BAD727E3F472DCB7BAC7078A69EFB748F242CF6EE74473045022100CC48021620B52E3F40F74BA45B3C89089C4580154EAF1027FEED92E6D76705AA0220069112B3017B327245E4B1258A83D7DF8737EFB83716617FDB2337E9CC6490CB8114DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47831460B1227191135B3B16CB1D74F2509BD5C5DF985B", so.ToHex());

        }


        [TestMethod]
        public void TestSerializeOfferCreateTxData_Sell()
        {
            var data = new OfferCreateTxData();
            data.Flags = 0;
            data.Fee = (decimal)0.0001;
            data.SigningPubKey = "02A8D70000DCCDAE1E639E5938559B840FA865F5C5B31ADCCE76EF51A7F71039D5";
            data.Sequence = 9;
            data.TransactionType = TransactionType.OfferCreate;

            data.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            data.TakerPays = "0.05";
            data.TakerGets= new AmountSettings { Currency = "CNY", Issuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", Value = "12.33" };
            data.Flags = (UInt32)OfferCreateFlags.Sell;

            var so = Serializer.Create(data);
            Assert.AreEqual("1200072200080000240000000964400000000000C35065D4C461682F021000000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D41596684000000000000064732102A8D70000DCCDAE1E639E5938559B840FA865F5C5B31ADCCE76EF51A7F71039D58114DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47", so.ToHex());
        }

        [TestMethod]
        public void TestSerializeOfferCreateTxData_Buy()
        {
            var data = new OfferCreateTxData();
            data.Flags = 0;
            data.Fee = (decimal)0.0001;
            data.SigningPubKey = "02A8D70000DCCDAE1E639E5938559B840FA865F5C5B31ADCCE76EF51A7F71039D5";
            data.Sequence = 9;
            data.TransactionType = TransactionType.OfferCreate;

            data.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            data.TakerGets = "0.05";
            data.TakerPays = new AmountSettings { Currency = "CNY", Issuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", Value = "12.33" };

            var so = Serializer.Create(data);
            Assert.AreEqual("1200072200000000240000000964D4C461682F021000000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D4159665400000000000C350684000000000000064732102A8D70000DCCDAE1E639E5938559B840FA865F5C5B31ADCCE76EF51A7F71039D58114DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47", so.ToHex());
        }

        [TestMethod]
        public void TestSerializeOfferCancelTxData()
        {
            var data = new OfferCancelTxData();
            data.Flags = 0;
            data.Fee = (decimal)0.0001;
            data.SigningPubKey = "02A8D70000DCCDAE1E639E5938559B840FA865F5C5B31ADCCE76EF51A7F71039D5";
            data.Sequence = 9;
            data.TransactionType = TransactionType.OfferCancel;

            data.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            data.OfferSequence = 7;

            var so = Serializer.Create(data);
            Assert.AreEqual("12000822000000002400000009201900000007684000000000000064732102A8D70000DCCDAE1E639E5938559B840FA865F5C5B31ADCCE76EF51A7F71039D58114DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47", so.ToHex());
        }

        [TestMethod]
        public void TestSerializeRelationTxData_TrustSet()
        {
            var data = new RelationTxData();
            data.Flags = 0;
            data.Fee = (decimal)0.0001;
            data.SigningPubKey = "0204B7DE11FDC08FBBC007000BAD727E3F472DCB7BAC7078A69EFB748F242CF6EE";
            data.Sequence = 9;
            data.TransactionType = TransactionType.TrustSet;

            data.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            data.QualityIn = 1;
            data.QualityOut = 2;
            data.LimitAmount = new AmountSettings { Currency = "CNY", Issuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", Value = "12.33" };

            var so = Serializer.Create(data);
            Assert.AreEqual("1200142200000000240000000920140000000120150000000263D4C461682F021000000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D4159668400000000000006473210204B7DE11FDC08FBBC007000BAD727E3F472DCB7BAC7078A69EFB748F242CF6EE8114DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47", so.ToHex());
        }

        [TestMethod]
        public void TestSerializeRelationTxData_RelationSet()
        {
            var data = new RelationTxData();
            data.Flags = 0;
            data.Fee = (decimal)0.0001;
            data.SigningPubKey = "0204B7DE11FDC08FBBC007000BAD727E3F472DCB7BAC7078A69EFB748F242CF6EE";
            data.Sequence = 9;
            data.TransactionType = TransactionType.RelationSet;

            data.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            data.Target = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            data.RelationType = "3"; //1->authorize, 3->freeze
            data.LimitAmount = new AmountSettings { Currency = "CNY", Issuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", Value = "12.33" };

            var so = Serializer.Create(data);
            Assert.AreEqual("1200152200000000240000000920230000000363D4C461682F021000000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D4159668400000000000006473210204B7DE11FDC08FBBC007000BAD727E3F472DCB7BAC7078A69EFB748F242CF6EE8114DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47871460B1227191135B3B16CB1D74F2509BD5C5DF985B", so.ToHex());
        }

        [TestMethod]
        public void TestSerializeRelationTxData_RelationDel()
        {
            var data = new RelationTxData();
            data.Flags = 0;
            data.Fee = (decimal)0.0001;
            data.SigningPubKey = "0204B7DE11FDC08FBBC007000BAD727E3F472DCB7BAC7078A69EFB748F242CF6EE";
            data.Sequence = 9;
            data.TransactionType = TransactionType.RelationDel;

            data.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            data.Target = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            data.RelationType = "1"; //1->authorize, 3->freeze
            data.LimitAmount = new AmountSettings { Currency = "CNY", Issuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", Value = "12.33" };

            var so = Serializer.Create(data);
            Assert.AreEqual("1200162200000000240000000920230000000163D4C461682F021000000000000000000000000000434E590000000000A582E432BFC48EEDEF852C814EC57F3CD2D4159668400000000000006473210204B7DE11FDC08FBBC007000BAD727E3F472DCB7BAC7078A69EFB748F242CF6EE8114DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47871460B1227191135B3B16CB1D74F2509BD5C5DF985B", so.ToHex());
        }

        [TestMethod]
        public void TestSerializeAccountSetTxData_AccountSet()
        {
            var data = new AccountSetTxData();
            data.Flags = 0;
            data.Fee = (decimal)0.0001;
            data.SigningPubKey = "0204B7DE11FDC08FBBC007000BAD727E3F472DCB7BAC7078A69EFB748F242CF6EE";
            data.Sequence = 9;
            data.TransactionType = TransactionType.AccountSet;

            data.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            data.SetFlag = (UInt32)SetClearFlags.DisallowSWT; //3
            data.ClearFlag = (UInt32)SetClearFlags.NoFreeze; // 6

            var so = Serializer.Create(data);
            Assert.AreEqual("1200032200000000240000000920210000000320220000000668400000000000006473210204B7DE11FDC08FBBC007000BAD727E3F472DCB7BAC7078A69EFB748F242CF6EE8114DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47", so.ToHex());
        }

        [TestMethod]
        public void TestSerializeAccountSetTxData_SetRegularKey()
        {
            var data = new AccountSetTxData();
            data.Flags = 0;
            data.Fee = (decimal)0.0001;
            data.SigningPubKey = "0204B7DE11FDC08FBBC007000BAD727E3F472DCB7BAC7078A69EFB748F242CF6EE";
            data.Sequence = 9;
            data.TransactionType = TransactionType.SetRegularKey;

            data.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            data.RegularKey = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";

            var so = Serializer.Create(data);
            Assert.AreEqual("1200052200000000240000000968400000000000006473210204B7DE11FDC08FBBC007000BAD727E3F472DCB7BAC7078A69EFB748F242CF6EE8114DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47881460B1227191135B3B16CB1D74F2509BD5C5DF985B", so.ToHex());
        }

        [TestMethod]
        public void TestSerializeConfigContract_Deploy()
        {
            var data = new ContractTxData();
            data.Flags = 0;
            data.Fee = (decimal)0.0001;
            data.SigningPubKey = "0204B7DE11FDC08FBBC007000BAD727E3F472DCB7BAC7078A69EFB748F242CF6EE";
            data.Sequence = 9;
            data.TransactionType = TransactionType.ConfigContract;

            data.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            data.Method = 0;
            data.Payload = "726573756C743D7B7D3B2066756E6374696F6E20496E697428742920726573756C743D73634765744163636F756E74496E666F2874292072657475726E20726573756C7420656E643B2066756E6374696F6E20666F6F28742920613D7B7D20726573756C743D73634765744163636F756E74496E666F2874292072657475726E20726573756C7420656E643B";
            data.Amount = 35;
            data.Args = new ArgInfo[] { new ArgInfo { Arg = new ParameterInfo { Parameter = "6A4D773378726B5832795377645169456F72796D7975544C55535361383577765372" } } };

            var so = Serializer.Create(data);
            Assert.AreEqual("12001E22000000002400000009202400000000614000000002160EC068400000000000006473210204B7DE11FDC08FBBC007000BAD727E3F472DCB7BAC7078A69EFB748F242CF6EE7F8C726573756C743D7B7D3B2066756E6374696F6E20496E697428742920726573756C743D73634765744163636F756E74496E666F2874292072657475726E20726573756C7420656E643B2066756E6374696F6E20666F6F28742920613D7B7D20726573756C743D73634765744163636F756E74496E666F2874292072657475726E20726573756C7420656E643B8114DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47FAEB7012226A4D773378726B5832795377645169456F72796D7975544C55535361383577765372E1F1", so.ToHex());
        }

        [TestMethod]
        public void TestSerializeConfigContract_Call()
        {
            var data = new ContractTxData();
            data.Flags = 0;
            data.Fee = (decimal)0.0001;
            data.SigningPubKey = "0204B7DE11FDC08FBBC007000BAD727E3F472DCB7BAC7078A69EFB748F242CF6EE";
            data.Sequence = 9;
            data.TransactionType = TransactionType.ConfigContract;

            data.Account = "jMw3xrkX2ySwdQiEorymyuTLUSSa85wvSr";
            data.Destination = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1";
            data.Method = 1;
            data.ContractMethod = "666F6F";
            data.Args = new ArgInfo[] { new ArgInfo { Arg = new ParameterInfo { Parameter = "6A4D773378726B5832795377645169456F72796D7975544C55535361383577765372" } } };

            var so = Serializer.Create(data);
            Assert.AreEqual("12001E2200000000240000000920240000000168400000000000006473210204B7DE11FDC08FBBC007000BAD727E3F472DCB7BAC7078A69EFB748F242CF6EE701103666F6F8114DD1CE7A2B5C266CC3F4E83CFF6B27C1A89A48F47831460B1227191135B3B16CB1D74F2509BD5C5DF985BFAEB7012226A4D773378726B5832795377645169456F72796D7975544C55535361383577765372E1F1", so.ToHex());
        }
    }
}