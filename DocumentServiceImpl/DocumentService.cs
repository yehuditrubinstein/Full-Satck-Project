using Contracts;
using DocumentContracts.DTO;
using DocumentContracts.DTO.Document;
using DocumentContracts.DTO.DocumentSharing;
using DocumentContracts.Interfaces;
using DocumentContracts.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace DocumentServiceImpl
{
    [Register(Policy.Transient,typeof(IDocumentSerivce))]
    public class DocumentService : IDocumentSerivce
    {
        private IDocumentDAL _DAL;
        private IDocumentSharingService _IDocumentSharingService;
        public DocumentService(IDocumentDAL DAL,IDocumentSharingService DocumentSharingService)
        {
            _DAL = DAL;
            _IDocumentSharingService = DocumentSharingService;
        }
        public DocumentResponse AddDocument(DocumentRequest request)
        {
            DocumentResponse response = default;

            if (Available(request))
            {
              
              
                    response = _DAL.AddDocument(request);
                if (response is DocumentResponseAddOK)
                    
                     _IDocumentSharingService.AddSharing(new DocumentSharingRequest() { sharingDTO = new SharingDTO() { DocID = response.documentDTO[0].DocID, UserId = request.documentDTO.UserID } });
                else
                    response = new DocumentResponseDontAdd(); 
            }

            else
            {
                response = new DocumentResponseDontAdd();
            }
            return response;

        }

        public DocumentResponse GetDocument(Guid docID)
        {
            DocumentResponse retval = default;

            if (docID!=null)
            {
                retval = _DAL.GetDocument(docID);

            }
            return retval;
        }

        public DocumentResponse GetDocumentForUser(DocumentRequestGetForUser request)
        {
            DocumentResponse retval = default;

            if (request.UserID!=""&& request.UserID != null)
            {
              retval=_DAL.GetDocumentsForUser(request);
            }
             
            
            return retval;
        }
        
        public DocumentResponse RemoveDocument(DocumentRequestRemove docID)
        {
            DocumentResponse retval = default;

            if (docID != null)
            {
                retval = _DAL.Removedocument(docID);

            }
            return retval;
        }

        private bool Available(DocumentRequest request)
        {
            return true;
        }
    }
}
