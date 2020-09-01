using DocumentContracts.DTO;
using DocumentContracts.DTO.DocumentSharing;

namespace DocumentContracts.Interfaces
{
    public interface IDocumentSharingDAL
    {
        DocumentsharingResponse AddSharing(DocumentSharingRequest request);
        DocumentsharingResponse RemoveSharing(DocumentSharingRequest request);
    }
}
