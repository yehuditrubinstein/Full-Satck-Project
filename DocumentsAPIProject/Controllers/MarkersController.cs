using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentContracts.DTO;
using DocumentContracts.DTO.Markers;
using DocumentContracts.Interfaces.Markers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsAPIProject.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class MarkersController : ControllerBase
    {
        IMarkersService _service;
        public MarkersController(IMarkersService service)
        {
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
    }
}
