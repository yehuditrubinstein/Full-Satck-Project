using DalParametersConverter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.DTO.Comment
{
    public class CommentRequestGetByDocID
    {
       [DBParameter("DocID")]
        public Guid DocID { get; set; }
    }
}
