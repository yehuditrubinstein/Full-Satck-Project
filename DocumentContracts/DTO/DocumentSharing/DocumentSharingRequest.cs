using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.DTO
{
   public class DocumentSharingRequest
    {
        public Guid DocID { get; set; }
        public string UserId { get; set; }
    }
}
