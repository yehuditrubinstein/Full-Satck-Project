using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTO;
using DocumentContracts.DTO;
using DocumentContracts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsAPIProject.Controllers
{

    [Route("api/[controller]/{action}")]
    [ApiController]
    public class DocumentSharingController : ControllerBase
    {
        IDocumentSharingService _Service;
        public DocumentSharingController(IDocumentSharingService Sharingservice)
        {
            _Service = Sharingservice;
        }
        [HttpPost]
        public DocumentContracts.DTO.Response AddSharing([FromBody] DocumentSharingRequest request)
        {
            return _Service.AddSharing(request);
        }
        [HttpPost]
        public DocumentContracts.DTO.Response RemoveSharing([FromBody] DocumentSharingRequest request)
        {
            return _Service.RemoveSharing(request);
        }
    }
}
