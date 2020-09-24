using DalParametersConverter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.DTO.Document
{
    public class DocumentDTO
    {
        [DBParameter("UserID")]
        public string UserID { get; set; }
        [DBParameter("ImageURL")]
        public string ImageURL { get; set; }
        [DBParameter("DocName")]
        public string DocName { get; set; }
        //[DBParameter("DocID")]
        public Guid DocID { get; set; }
    }
}
