using Contracts;
using DocumentContracts.DTO.Comment;
using DocumentContracts.DTO.DocumentSharing;
using DocumentContracts.DTO.Markers;
using DocumentContracts.Interfaces;
using DocumentContracts.Interfaces.Comment;
using DocumentContracts.Interfaces.Markers;
using MessangerContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentServiceImpl
{
    [Register(Policy.Transient, typeof(ICommentService))]
    public class CommentService : ICommentService
    {
        ICommentDAL _DAL;
        private IDocumentSharingService _documentSharingService;
        IMessanger _messanger;
        IMarkersDAL _markersDAL;

        public CommentService(IMarkersDAL markersDAL, ICommentDAL dal,IDocumentSharingService documentSharingService, IMessanger messanger)
        {
            _DAL = dal;
            _documentSharingService = documentSharingService;
            _messanger = messanger;
            _markersDAL = markersDAL;
        }
        public CommentResponse CreateComment(CommentRequest request)
        {
            List<DocumentSharingDTO> shared = default;
            List<string> mylist = new List<string>();
            CommentResponse retval = default;
            if (IsCommentAvailable(request))
            {
                retval = _DAL.CreateComment(request);
                shared = _documentSharingService.GetShareForDoc(new DocumentSharingRequestGetForDoc() { DocID = _markersDAL.GetMarkerByID(new RequestGetMarkers() { DocID=request.commentDTO.MarkerID}).Markers[0].DocID }).DocumentSharingDTO;
                if (shared != null)
                {
                    //create list type string for send to all
                    shared.ForEach(s => mylist.Add(s.UserId));
                    mylist.Remove(request.commentDTO.UserId);
                    _messanger.SendMarkerToAll(mylist, retval);
                }
            }
            return retval;
        }
        public CommentResponse DeleteComment(CommentRequestDelete request)
        {
            List<DocumentSharingDTO> shared = default;
            List<string> mylist = new List<string>();
            CommentResponse retval = default;
            if (IsDeleteAvailable(request.CommentID))
            {
                retval = _DAL.DeleteComment(request);
                shared = _documentSharingService.GetShareForDoc(new DocumentSharingRequestGetForDoc() { DocID = request.DocID }).DocumentSharingDTO;
                if (shared != null)
                {
                    //create list type string for send to all
                    shared.ForEach(s => mylist.Add(s.UserId));
                    _messanger.SendMarkerToAll(mylist, retval);
                }
            }
            return retval;
        }
        public CommentResponse GetCommentsForDocument(CommentRequestGetByDocID request)
        {
            CommentResponse retval = default;
            if (request.DocID != null && request.DocID != Guid.Empty)
            {
                retval = _DAL.GetCommentsForDocument(request);
            }
            return retval;
        }
        bool IsDeleteAvailable(Guid id)
        {
            //    if(id!=Guid.Empty)
            //        מי שמנסה למחוק הוא בעל ההערה
            return true;
         }
        bool IsCommentAvailable(CommentRequest req)
        {
            //validations
            return true;
        }

       
    }
}
