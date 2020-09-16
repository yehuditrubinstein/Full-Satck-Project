using Contracts;
using DocumentContracts.DTO.Document;
using DocumentContracts.Interfaces;
using DocumentContracts.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentServiceImpl
{
    [Register(Policy.Transient,typeof(IDocumentSerivce))]
    public class DocumentService : IDocumentSerivce
    {
        private IDocumentDAL _DAL;
        public DocumentService(IDocumentDAL DAL)
        {
            _DAL = DAL;
        }
        public DocumentResponse AddDocument(DocumentRequest request)
        {
            DocumentResponse response = default;

            if (Available(request))
            {
                response = _DAL.AddDocument(request);
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

        public DocumentResponse GetDocumentForUser(string UserID)
        {
            DocumentResponse retval = default;

            if (UserID!=""&&UserID!=null)
            {
              retval=_DAL.GetDocumentsForUser(UserID);
            }
             
            
            return retval;
        }

        public DocumentResponse RemoveDocument(Guid docID)
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
