using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Forms
{
    public class ResetPassForm
    {
        public string email { set; get; }
        public string newpass { set; get; }
        public string repass { set; get; }
        public string opt { set; get; }
    }
}
