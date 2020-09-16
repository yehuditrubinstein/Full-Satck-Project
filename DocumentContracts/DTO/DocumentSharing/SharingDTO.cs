using DalParametersConverter;
using System;

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
