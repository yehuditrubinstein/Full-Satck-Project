using DocumentContracts.DTO.Document;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.Interfaces.Document
{
    public interface IDocumentSerivce
    {
        DocumentResponse AddDocument(DocumentRequest request);
        DocumentResponse GetDocument(Guid docID);
        DocumentResponse RemoveDocument(DocumentRequestRemove docID);
        DocumentResponse GetDocumentForUser(DocumentRequestGetForUser request);
    }
}
