using MessangerContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace DocumentsAPIProject
{
    public class WebSocketAdapter:ISocket
    {
        public WebSocket Socket { get; set; }
    }
}
