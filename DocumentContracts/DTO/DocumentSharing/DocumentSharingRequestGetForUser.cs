using DalParametersConverter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.DTO.DocumentSharing
{
    public class DocumentSharingRequestGetForUser
    {
        [DBParameter("userID")]
        public string userID { get; set; }
    }
}
