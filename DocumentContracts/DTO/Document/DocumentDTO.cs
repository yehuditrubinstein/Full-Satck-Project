using DalParametersConverter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.DTO.Document
{
    public class DocumentDTO
    {
        public DocumentDTO()
        {
            ImageURL= @"C:\Users\מוזסון\source\repos\Full-Satck-Project\DocumentsAPIProject\Images\" + DocName + ".png";
        }
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
