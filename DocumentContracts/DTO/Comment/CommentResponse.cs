using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.DTO.Comment
{
    public class CommentResponse:Response
    {
        public List<CommentDTO> comments { get; set; }
    }
}
