using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BackendDotnetCore.Enitities
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Active { get; set; }
        public int Delete { get; set; }
        public int Level { get; set; }
        public string Avatar { get; set; }

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
