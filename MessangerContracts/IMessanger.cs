using DocumentContracts.DTO.Markers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessangerContracts
{
   public interface IMessanger
    {
        Task Send(string id, MessageBody messageBody);
        Task SendtoAll(List<string> id, MessageBody messageBody);
        Task SendMarkerToAll(List<string> id, MarkerDTO marker);
        Task<IReceiver> Add(string id, ISocket socket);
    }
}
