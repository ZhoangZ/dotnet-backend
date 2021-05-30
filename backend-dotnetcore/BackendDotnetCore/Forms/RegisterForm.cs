using BackendDotnetCore.Entities;
using BackendDotnetCore.Ultis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Forms
{
    public class RegisterForm
    {
        public string password { set; get; }
        public string repassword { set; get; }
        public string email { set; get; }
        public string fullname { set; get; }
        public string phone { set; get; }
        public string address { set; get; }

        public string checkInfo()
        {
            if (!password.Equals(repassword)) return "Mật khẩu không trùng khớp!";
            if (null == email || email.Equals("")) return "Email không được để trống!";
            if (null == password || password.Equals("")) return "Mật khẩu không được để trống!";
            return "success";
        }
       
        public UserEntity parseEntity()
        {
            UserEntity ue = new UserEntity();
            ue.Email = email;
            ue.Fullname = fullname;
            ue.Password = password;
            ue.phone = phone;
            ue.address = address;
            return ue;
        }

    }
}
