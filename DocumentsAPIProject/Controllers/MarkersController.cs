using DocumentContracts.DTO.Markers;
using DocumentContracts.Interfaces.Markers;
using MessangerContracts;
using Microsoft.AspNetCore.Mvc;

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
        public MarkerRsponse AddMarker([FromBody]MarkerRequestAdd request) => _service.AddMarker(request);
        [HttpPost]
        public MarkerRsponse RemoveMarker([FromBody] MarkerRequestRemove request) => _service.RemoveMarker(request);
        [HttpPost]
        public MarkerRsponse GetMarkers([FromBody]RequestGetMarkers request) => _service.GetMarkers(request);
        
    }
}
