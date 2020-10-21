using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentContracts.DTO.Document;
using DocumentContracts.Interfaces.Document;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace DocumentsAPIProject.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        const string url = @"C:\Users\מוזסון\source\repos\Full-Satck-Project\DocumentsAPIProject\Images\";
        private IDocumentSerivce _Service;
        public DocumentController(IDocumentSerivce service) => _Service = service;
        [HttpPost]
        public DocumentResponse AddDocument([FromBody] DocumentRequest request)
        {
            request.documentDTO.ImageURL = $"{url}{request.documentDTO.DocName}.png";
            return _Service.AddDocument(request);
        }
        [HttpGet]
        public DocumentResponse GetDocument(Guid DocID) => _Service.GetDocument(DocID);
        [HttpPost]
        public DocumentResponse GetDocumentsForUser([FromBody] DocumentRequestGetForUser request) => _Service.GetDocumentForUser(request);
        [HttpPost]
        public DocumentResponse RemoveDocument([FromBody] DocumentRequestRemove request) => _Service.RemoveDocument(request);
        [HttpPost]
     //   [Route("[action]/{FileName}")]
        public async Task<IActionResult> UploadFile(string name)
        {
            var files = Request.Form.Files;
            var FileName = Request.Form.Keys.FirstOrDefault();
            try
            {
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        using (var stream = new FileStream(@"C:\Users\מוזסון\source\repos\Full-Satck-Project\DocumentsAPIProject\Images\" + FileName + ".png", FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                            }
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                //no actual error handling
                return Ok();
            }
        }
    }
}
