using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentContracts.DTO.Comment;
using DocumentContracts.Interfaces.Comment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsAPIProject.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        ICommentService _Service;
        public CommentController(ICommentService Service) => _Service = Service;
        [HttpPost]
        public CommentResponse AddComment([FromBody] CommentRequest request) => _Service.CreateComment(request);
        [HttpPost]
        public CommentResponse GetCommentsForDoc(CommentRequestGetByDocID DocID) => _Service.GetCommentsForDocument(DocID);
        [HttpPost]
        public CommentResponse DeleteComment(CommentRequestDelete request) => _Service.DeleteComment(request);
    }
}
