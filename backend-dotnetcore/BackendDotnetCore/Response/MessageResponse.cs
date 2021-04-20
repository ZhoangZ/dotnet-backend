using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Response
{
    public class MessageResponse
    {
        public Message Message { get; set; }
        public MessageResponse()
        {
            
        }
        public MessageResponse(string vi, string en)
        {
            this.Message = new Message(vi, en);
        }
    }
    public class Message
    {
        public string Vi { get; set; }
        public string En { get; set; }
        public Message()
        {
            
        }
        public Message(string vi, string en)
        {
            this.Vi = vi;
            this.En = en;
        }
    }
}
