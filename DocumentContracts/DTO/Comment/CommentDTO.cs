using DalParametersConverter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.DTO
{
    public class CommentDTO
    {
        //[DBParameter("CommentID")]
        public Guid CommentID { get; set; }
        [DBParameter("UserId")]
        public string UserId { get; set; }
        [DBParameter("MarkerID")]
        public Guid MarkerID { get; set; }
        [DBParameter("Content")]
        public string Content { get; set; }
        [DBParameter("CommentDate")]
        public DateTime CommentDate { get; set; }
    }
}
