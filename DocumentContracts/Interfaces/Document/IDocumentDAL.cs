using DocumentContracts.DTO.Document;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.Interfaces
{
    public interface IDocumentDAL
    {
        DocumentResponse AddDocument(DocumentRequest req);
        DocumentResponse Removedocument(Guid docID);
        DocumentResponse GetDocument(Guid docID);
        DocumentResponse GetDocumentsForUser(string userID);
    }
}
