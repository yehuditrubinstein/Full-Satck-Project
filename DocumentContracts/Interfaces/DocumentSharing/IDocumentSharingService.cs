using DocumentContracts.DTO;
using DocumentContracts.DTO.DocumentSharing;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.Interfaces
{
    public interface IDocumentSharingService
    {

        DocumentsharingResponse AddSharing(DocumentSharingRequest request);
        DocumentsharingResponse RemoveSharing(DocumentSharingRequest request);
    }
}
