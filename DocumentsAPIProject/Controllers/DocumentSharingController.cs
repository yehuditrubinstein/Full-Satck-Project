using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTO;
using DocumentContracts.DTO;
using DocumentContracts.DTO.DocumentSharing;
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
        public DocumentsharingResponse AddSharing([FromBody] DocumentSharingRequest request)
        {
            return _Service.AddSharing(request);
        }
        [HttpPost]
        public DocumentsharingResponse RemoveSharing([FromBody] DocumentSharingRequest request)
        {
            return _Service.RemoveSharing(request);
        }
    }
}
