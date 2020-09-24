using DocumentContracts.DTO;
using DocumentContracts.DTO.DocumentSharing;
using System;

namespace DocumentContracts.Interfaces
{
    public interface IDocumentSharingDAL
    {
        DocumentsharingResponse AddSharing(DocumentSharingRequest request);
        DocumentsharingResponse RemoveSharing(DocumentSharingRequest request);
        DocumentsharingResponse GetShareForDoc(Guid DocID);
        DocumentsharingResponse GetShareForUser(string userID);

    }
}
