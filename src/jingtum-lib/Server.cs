using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocket4Net;

namespace JingTum.Lib
{
    internal class Server : IDisposable
    {
        private Remote _remote;
        private string _url;
        private WebSocket _ws;
        private bool _connected;
        private bool _opened;
        private ServerState _state = ServerState.Offline;
        private int _id;
        private Timer _timer;
        private static object _locker = new object();

        public Server(Remote remote, string url)
        {
            this._url = url;
            this._remote = remote;
        }

        public virtual bool IsConnected
        {
            get { return _connected; }
        }

        public void Dispose()
        {
            Disconnect();
        }

        public async virtual void Connect(MessageCallback<ConnectResponse> callback, Func<object, ConnectResponse> filter = null)
        {
            await ConnectAsync(callback, filter);
        }

        internal async Task<MessageResult<ConnectResponse>> ConnectAsync(MessageCallback<ConnectResponse> callback, Func<object, ConnectResponse> filter = null, int timeout = 60000)
        {
            if (_connected) return null;

            lock (_locker)
            {
                // already connected
                if (_ws != null && (_ws.State == WebSocketState.Connecting || _ws.State == WebSocketState.Open))
                {
                    return null;
                }
            }

            var resetEvent = new AutoResetEvent(false);

            MessageResult<ConnectResponse> result = null;
            MessageCallback<ConnectResponse> callbackWrapper = r =>
            {
                result = r;
                callback?.Invoke(r);
                resetEvent.Set();
            };

            var task = new Task(() =>
            {
                if (!resetEvent.WaitOne(timeout))
                {
                    callbackWrapper(new MessageResult<ConnectResponse>(null, new TimeoutException()));
                }
                resetEvent.Dispose();
            });
            task.Start();

            try
            {
                var url = new Uri(_url);
            }
            catch (Exception ex)
            {
                callbackWrapper(new MessageResult<ConnectResponse>(null, ex));
                return result;
            }

            Disconnect();

            if (_ws == null)
            {
                lock (_locker)
                {
                    if (_ws == null)
                    {
                        try
                        {
                            _ws = new WebSocket(_url);
                            _ws.Opened += (s, e) =>
                            {
                                _opened = true;
                                _remote.Subscribe<ConnectResponse>("ledger", "server").SetFilter(filter).Submit(callbackWrapper);
                            };
                            _ws.Closed += OnWSClosed;
                            _ws.Error += (s, e) =>
                            {
                                callbackWrapper(new MessageResult<ConnectResponse>(null, e.Exception));
                            };
                            _ws.MessageReceived += OnWSMessage;
                        }
                        catch (Exception ex)
                        {
                            callbackWrapper(new MessageResult<ConnectResponse>(null, ex));
                        }

                        _ws.Open();
                    }
                }
            }

            await task;
            return result;
        }

        public virtual void Disconnect()
        {
            if (_ws != null)
            {
                SetState(ServerState.Offline);
                if (_ws.State == WebSocketState.Connecting || _ws.State == WebSocketState.Open)
                {
                    _ws.Close();
                }
                _ws.Dispose();
                _ws = null;
            }
        }

        public virtual int SendMessage(string command, dynamic data)
        {
            if (!_opened) return -1;

            var id = _id++;

            var settings = new JsonSerializerSettings
            {
                //ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
            data.id = id;
            data.command = command;
            var message = JsonConvert.SerializeObject(data, settings);
            _ws.Send(message);
            return id;
        }

        internal void SetState(ServerState state)
        {
            _state = state;
            _connected = state == ServerState.Online;
            if (!_connected)
            {
                _opened = false;
            }
        }

        private void OnWSMessage(object sender, MessageReceivedEventArgs e)
        {
            var data = e.Message;
            try
            {
                _remote.HandleMessage(data);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void OnWSClosed(object sender, EventArgs e)
        {
            if (_state == ServerState.Offline) return;

            SetState(ServerState.Offline);
            if (_timer != null) return;

            _remote.OnDisconnected();
            _timer = new Timer(new TimerCallback((state) =>
            {
                if (_ws != null && _ws.State == WebSocketState.Connecting)return;

                if (_ws != null && _ws.State == WebSocketState.Open)
                {
                    if (_timer != null)
                    {
                        _timer.Dispose();
                        _timer = null;
                    }
                    return;
                }

                this.Connect((result) =>
                    {
                        if (result.Exception == null && result.Result != null &&
                            (result.Result.LedgerIndex > 0 || result.Result.HostId != null))
                        {
                            if (_timer != null)
                            {
                                _timer.Dispose();
                                _timer = null;
                            }

                            _remote.OnReconnected();
                        }
                    });
            }), null, 3000, System.Threading.Timeout.Infinite);

            return;
        }
    }
}
