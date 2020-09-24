using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentContracts.DTO;
using DocumentContracts.DTO.Markers;
using DocumentContracts.Interfaces.Markers;
using MessangerContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocumentsAPIProject.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class MarkersController : ControllerBase
    {
        IMarkersService _service;
        IMessanger _messanger;

        public MarkersController(IMarkersService service, IMessanger messanger)
        {
            _messanger = messanger;
            _service = service;
        }

        [HttpPost]
        public MarkerRsponse AddMarker(MarkerRequestAdd request)
        {
            return _service.AddMarker(request);
        }
        [HttpDelete]
        public MarkerRsponse RemoveMarker(MarkerRequestRemove request)
        {
            return _service.RemoveMarker(request);
        }
        [HttpPost]
        public MarkerRsponse GetMarkers(RequestGetMarkers request)
        {
            return _service.GetMarkers(request);
        }
        //[HttpGet]
        //public void trySend()
        //{
        //     TRYSendChanges(new MessageRequest() { ID = "1", MessageBody = new MessageBody() { Code = "stam" } });
        //}
        //public  void TRYSendChanges(MessageRequest messageRequest)
        //{
        //    List<string> list = new List<string>() { "2@gmail.com", "4@gmail.com", "6@gmail.com" };
        //     _messanger.SendtoAll(list, messageRequest.MessageBody);
        //}
    }
}
