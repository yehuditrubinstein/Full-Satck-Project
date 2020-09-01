using DalParametersConverter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.DTO.DocumentSharing
{
    public class SharingDTO
    {
        [DBParameter("DocID")]
        public Guid DocID { get; set; }
        [DBParameter("UserId")]
        public string UserId { get; set; }
    }
}
