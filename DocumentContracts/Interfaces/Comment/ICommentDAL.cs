using DocumentContracts.DTO.Comment;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.Interfaces.Comment
{
    public interface ICommentDAL
    {
        CommentResponse CreateComment(CommentRequest request);
        CommentResponse DeleteComment(CommentRequestDelete request);
        CommentResponse GetCommentsForDocument(CommentRequestGetByDocID DocID);
    }
}
