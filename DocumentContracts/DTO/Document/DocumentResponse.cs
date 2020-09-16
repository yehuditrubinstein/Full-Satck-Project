using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.DTO.Document
{
    public class DocumentResponse:Response
    {
        public List<DocumentDTO> documentDTO { get; set; }
    }
}
