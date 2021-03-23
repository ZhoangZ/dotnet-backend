using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Response
{
    public class MessageResponse
    {
        public string Message { get; set; }
        public MessageResponse(string mes)
        {
            this.Message = mes;
        }
    }
}
