using DalParametersConverter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.DTO.Document
{
   public class DocumentRequestGetForUser
    {
        [DBParameter("UserID")]
        public string UserID { get; set; }
    }
}
