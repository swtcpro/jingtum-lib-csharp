using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JingTum.Lib;

namespace UnitTests
{
    internal class MockServer : Server
    {
        private Remote _remote;
        private string _jsonFile;
        private bool _connected;

        public MockServer(Remote remote, string jsonFile, bool connected = true):base(remote, "")
        {
            _remote = remote;
            _jsonFile = jsonFile;
            _connected = connected;
        }

        public override bool IsConnected => _connected;

        public override void Connect(MessageCallback<ConnectResponse> callback, Func<object, ConnectResponse> filter = null)
        {
            if (_connected) return;

            _remote.Subscribe<ConnectResponse>("ledger", "server").SetFilter(filter).Submit(callback);
            _connected = true;
        }

        public override void Disconnect()
        {
            _connected = false;
        }

        public override int SendMessage(string command, dynamic data)
        {
            var filePath = System.IO.Path.Combine(Environment.CurrentDirectory, "..\\..\\", "JsonFiles", _jsonFile);
            var json = File.ReadAllText(filePath);
            var task = new Task(() =>
            {
                System.Threading.Thread.Sleep(200);
                _remote.HandleMessage(json);
            });
            task.Start();

            return 0;
        }
    }
}
