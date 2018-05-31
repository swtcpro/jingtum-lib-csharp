using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using JingTum.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    [TestCategory("Remote")]
    public class RemoteTests
    {
        private const int DeferredWaitingTime = 10000;
        private static string ServerUrl = @"ws://123.57.219.57:5020";
        private Remote _remote = null;

        [TestCleanup()]
        public void Cleanup()
        {
            if(_remote != null)
            {
                _remote.Dispose();
                _remote = null;
            }
        }

        [TestMethod]
        public void TestConnect()
        {
            var deferred = new Task(() => { });
            _remote = new Remote(ServerUrl);
            MessageResult<ConnectResponse> response = null;
            _remote.Connect(r =>
            {
                response = r;
                deferred.Start();
            });
            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsTrue(_remote.IsConnected);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.IsNotNull(response.Message);
            Assert.IsNull(response.Exception);
        }

        [TestMethod]
        public void TestConnectAsync()
        {
            _remote = new Remote(ServerUrl);
            var tc = _remote.ConnectAsync();
            Assert.IsTrue(tc.Wait(DeferredWaitingTime));

            Assert.IsTrue(_remote.IsConnected);
            var response = tc.Result;
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.IsNotNull(response.Message);
            Assert.IsNull(response.Exception);
        }

        [TestMethod]
        public void TestDisconnect()
        {
            var deferred = new Task(() => { });
            _remote = new Remote(ServerUrl);
            MessageResult<ConnectResponse> response = null;
            _remote.Connect(r =>
            {
                response = r;
                deferred.Start();
            });
            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsTrue(_remote.IsConnected);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);

            _remote.Disconnect();
            Assert.IsFalse(_remote.IsConnected);
        }

        [TestMethod]
        public void TestFailedConnect()
        {
            var deferred = new Task(() => { });
            var fakeUrl = @"ws://123.456.789.1:5020";
            _remote = new Remote(fakeUrl);
            MessageResult<ConnectResponse> response = null;
            _remote.Connect(r =>
            {
                response = r;
                deferred.Start();
            });
            Assert.IsTrue(deferred.Wait(DeferredWaitingTime));

            Assert.IsFalse(_remote.IsConnected);
            Assert.IsNull(response.Result);
            Assert.IsNull(response.Message);
            Assert.IsInstanceOfType(response.Exception, typeof(SocketException));
        }
    }
}
