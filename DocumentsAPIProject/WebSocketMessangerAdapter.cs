using DocumentContracts.DTO.Markers;
using MessangerContracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentsAPIProject
{
    public class WebSocketMessangerAdapter : IMessanger
    {
        Dictionary<string, WebSocket> _sockets;
        Dictionary<string, Receiver> _receivers;
        readonly ILogger<WebSocketMessangerAdapter> _logger;
        public WebSocketMessangerAdapter(ILogger<WebSocketMessangerAdapter> logger)
        {
            _logger = logger;
            _sockets = new Dictionary<string, WebSocket>();
        }
        public async Task<IReceiver> Add(string id, ISocket socket)
        {
            var webSocketAdapter = socket as WebSocketAdapter;
            Receiver retval = null;
            if (_sockets.ContainsKey(id))
            {
                // var cursocket = _sockets[id];
                _sockets.Remove(id);
                //      await cursocket.CloseAsync(WebSocketCloseStatus.Empty, "Remote Closed", CancellationToken.None);

            }
            _sockets.Add(id, webSocketAdapter.Socket);

            retval = new Receiver(webSocketAdapter.Socket);
            return retval;

        }

        public async Task Send(string id, MessageBody messageBody)
        {
            if (_sockets.ContainsKey(id))
            {
                var message = JsonConvert.SerializeObject(messageBody);
                var buffer = Encoding.UTF8.GetBytes(message);
                await _sockets[id].SendAsync(new ReadOnlyMemory<byte>(buffer), WebSocketMessageType.Text
                    , true
                   , CancellationToken.None);
            }
        }
        public async Task SendtoAll(List<string> id, MessageBody messageBody)
        {
            foreach (var item in id)
            {
                if (_sockets.ContainsKey(item))
                {
                    var message = JsonConvert.SerializeObject(messageBody);
                    var buffer = Encoding.UTF8.GetBytes(message);
                    await _sockets[item].SendAsync(new ReadOnlyMemory<byte>(buffer), WebSocketMessageType.Text
                        , true
                       , CancellationToken.None);
                }
            }

        }
        public async Task SendMarkerToAll(List<string> id, object messageBody)
        {
            foreach (var item in id)
            {
                if (_sockets.ContainsKey(item))
                {
                    var message = JsonConvert.SerializeObject(messageBody);
                    var buffer = Encoding.UTF8.GetBytes(message);
                    await _sockets[item].SendAsync(new ReadOnlyMemory<byte>(buffer), WebSocketMessageType.Text
                        , true
                       , CancellationToken.None);
                }
            }

        }
    }
}
