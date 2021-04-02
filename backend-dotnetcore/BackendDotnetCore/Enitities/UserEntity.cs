using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace BackendDotnetCore.Enitities
{
    [Table("users")]
    public partial class UserEntity
    {
        [Key]
        [Column("id")]
        public int Id { set; get; }
        [Column("username")]
        [Require]
        public string Username { set; get; }

        [Column("email")]
        [Require]
        public string Email { set; get; }

        [Column("provider")]
        public string Provider { set; get; }

        [Column("confirmed")]
        public int Confirmed { set; get; }
        [Column("blocked")]
        public string Blocked { set; get; }
        [Column("active")]
        public int Active { set; get; }




        //one to one (role)
        public virtual RoleEntity Role { get; set; }

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
