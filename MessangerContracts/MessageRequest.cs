using System;
using System.Collections.Generic;
using System.Text;

namespace MessangerContracts
{
    public class MessageBody
    {
        public string marker { get; set; }
    }
   public class MessageRequest
    {
        public string ID { get; set; }
        public MessageBody MessageBody { get; set; }
    }
}
