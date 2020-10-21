using DalParametersConverter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.DTO.DocumentSharing
{
   public class DocumentSharingRequestGetForDoc
    {
        [DBParameter("DocID")]

        public Guid DocID { get; set; }
    }
}
