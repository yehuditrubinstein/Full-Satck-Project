using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentContracts.DTO.Document;
using DocumentContracts.Interfaces.Document;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsAPIProject.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class DocumentController : ControllerBase
    {

        private IDocumentSerivce _Service;
        public DocumentController(IDocumentSerivce service)
        {
            _Service = service;
        }
        [HttpPost]
        public DocumentResponse AddDocument([FromBody]DocumentRequest request)
        {
            return _Service.AddDocument(request);
        }
        [HttpGet]
        public DocumentResponse GetDocument(Guid DocID)
        {
            return _Service.GetDocument(DocID);
        }
        [HttpGet]
        public DocumentResponse GetDocumentsForUser(string UserID)
        {
            return _Service.GetDocumentForUser(UserID);
        }
        [HttpDelete]
        public DocumentResponse RemoveDocument(Guid DocID)
        {
            return _Service.RemoveDocument(DocID);
        }
    }
}
