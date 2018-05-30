using JingTum.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    [TestCategory("Wallet")]
    public class WalletTests
    {
        [TestMethod]
        public void TestGenerate()
        {
            var wallet = Wallet.Generate();
            Assert.IsNotNull(wallet);
            Assert.AreEqual(34, wallet.Address.Length);
            Assert.AreEqual(29, wallet.Secret.Length);

            var wallet2 = Wallet.FromSecret(wallet.Secret);
            Assert.AreEqual(wallet2.Address, wallet.Address);
            Assert.AreEqual(wallet2.Secret, wallet.Secret);
        }

        [TestMethod]
        [DataRow("sn2xJy1QHRwCsp5EJ3kDXxT4EVgL2", "jfLgwrP2mBnK6RL25WBYRNgx692aeptiP2")]
        [DataRow("snyGVDPMPBrPA3rKWFX8G5S5syBvf", "jnEgy5cRVvtodhZgqeHWhcJAQsULpyw2SM")]
        public void TestFromSecret(string secret, string address)
        {
            var wallet = Wallet.FromSecret(secret);
            Assert.IsNotNull(wallet);
            Assert.AreEqual(secret, wallet.Secret);
            Assert.AreEqual(address, wallet.Address);
        }

        [TestMethod]
        [DataRow(null, typeof(ArgumentNullException), DisplayName ="Null")]
        [DataRow("", typeof(InvalidSecretException), DisplayName ="Empty")]
        [DataRow("jfLgwrP2mBnK6RL25WBYRNgx692aeptiP2", typeof(InvalidSecretException), DisplayName = "Address")]
        [DataRow("abc", typeof(InvalidSecretException), DisplayName = "Invalid")]
        public void TestInvalidSecret(string secret, Type exceptionType)
        {
            Exception exception = null;
            try
            {
                var wallet = Wallet.FromSecret(secret);
            }
            catch(Exception ex)
            {
                exception = ex;
            }

            if (exceptionType == null)
            {
                Assert.IsNull(exception);
            }
            else
            {
                Assert.IsNotNull(exception);
                Assert.IsTrue(exceptionType.IsAssignableFrom(exception.GetType()));
            }
        }
    }
}
