using DocumentContracts.DTO.Comment;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.Interfaces.Comment
{
    public interface ICommentService
    {
        CommentResponse GetCommentsForDocument(CommentRequestGetByDocID DocID);
        CommentResponse DeleteComment(CommentRequestDelete request);
        CommentResponse CreateComment(CommentRequest request);
    }
}
