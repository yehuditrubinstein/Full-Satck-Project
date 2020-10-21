using DalParametersConverter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.DTO.Document
{
   public class DocumentRequestRemove
    {
        [DBParameter("DocID")]
        public Guid DocID { get; set; }
    }
}
