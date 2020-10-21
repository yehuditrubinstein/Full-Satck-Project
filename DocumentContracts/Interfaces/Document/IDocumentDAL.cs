using DocumentContracts.DTO.Document;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.Interfaces
{
    public interface IDocumentDAL
    {
        DocumentResponse AddDocument(DocumentRequest req);
        DocumentResponse Removedocument(DocumentRequestRemove docID);
        DocumentResponse GetDocument(Guid DocID);
        DocumentResponse GetDocumentsForUser(DocumentRequestGetForUser request);
    }
}
