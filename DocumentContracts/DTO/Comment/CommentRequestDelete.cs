using DalParametersConverter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.DTO.Comment
{
    public class CommentRequestDelete
    {
        [DBParameter("CommentID")]
        public Guid CommentID { get; set; }
        public Guid DocID { get; set; }
    }
}
