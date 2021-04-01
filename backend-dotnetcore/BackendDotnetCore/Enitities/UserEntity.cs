using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BackendDotnetCore.Enitities
{
    public class UserEntity
    {
        [Column("id")]
        public int Id;
        [Column("username")]
        [Require]
        public string Username;

        [Column("email")]
        [Require]
        public string Email;

        [Column("provider")]
        public string Provider;
        
        [Column("confirmed")]
        public int Confirmed;
        [Column("blocked")]
        public string Blocked;
        [Column("active")]
        public int Active;
        [Column("role_id")]
        public int roleID;



        //one to one (role)
        //public virtual RoleEntity Role { get; set; }


        public UserEntity(int id, string username, string email, string provider, int confirmed, string blocked, int active, int roleID)
        {
            this.Id = id;
            this.Username = username;
            this.Email = email;
            this.Provider = provider;
            this.Confirmed = confirmed;
            this.Blocked = blocked;
            this.Active = active;
            this.roleID = roleID;



        }

        public override String ToString()
        {
            Type objType = this.GetType();
            PropertyInfo[] propertyInfoList = objType.GetProperties(BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            StringBuilder result = new StringBuilder();
            result.AppendFormat(objType.Name + "[");
            bool flag = false;
            foreach (PropertyInfo propertyInfo in propertyInfoList)
            {
                result.AppendFormat("{0}={1}, ", propertyInfo.Name, propertyInfo.GetValue(this));
                flag = true;
            }
            if (flag)
                result.Remove(result.Length - 2, 1);
            result.AppendFormat("]");
            return result.ToString();
        }

    }
}
