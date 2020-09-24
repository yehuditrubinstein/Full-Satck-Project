using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.DTO.DocumentSharing
{
    public class DocumentsharingResponse : Response
    {
        public List<DocumentSharingDTO> DocumentSharingDTO { get; set; }
    }
}
