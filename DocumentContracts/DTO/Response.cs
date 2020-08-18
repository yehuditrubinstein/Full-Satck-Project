using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.DTO
{
   public class Response
    {
        public string ResponseType { get; }
        public Response()
        {
            ResponseType = GetType().Name;
        }
    }
}
