using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Forms
{
    public class ResetPassForm
    {
        public int id { set; get; }
        public string currentPass { set; get; }
        public string email { set; get; }
        public string newpass { set; get; }
        public string repass { set; get; }
        public string opt { set; get; }



        public bool checkOldPass(string oldPass)
        {
            if (!currentPass.Equals(oldPass)) return false;
            return true;
        }

        public bool checkRepass()
        {
            if (!newpass.Equals(repass)) return false;
            return true;
        }
        public bool checkNewPassEqualsOldPass(string oldPass)
        {
            if (!newpass.Equals(oldPass)) return false;
            return true;
        }


    }
}
