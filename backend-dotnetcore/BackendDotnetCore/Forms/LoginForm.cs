using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BackendDotnetCore.Froms
{
    public class LoginForm

    {
        public string Username { get; set; }
        public string Email { set; get; }
        public string Password { get; set; }
        public bool RemenberMe { get; set; }

        public override String ToString()
        {
            Type objType = this.GetType();
            PropertyInfo[] propertyInfoList = objType.GetProperties();
            StringBuilder result = new StringBuilder();
            foreach (PropertyInfo propertyInfo in propertyInfoList)
                result.AppendFormat("{0}={1} ", propertyInfo.Name, propertyInfo.GetValue(this));

            return result.ToString();
        }
    }
}
