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
        public int Id { get; set; }
        [Column("username")]
        [Require]
        public string Username { get; set; }
        [Column("password")]
        [Require]
        public string Password { get; set; }

        [Column("email")]
        [Require]
        public string Email { get; set; }

        [Column("provider")]
        public string Provider { get; set; }

        [Column("confirmed")]
        public bool Confirmed { get; set; }
        [Column("blocked")]
        public bool Blocked { get; set; }
        [Column("active")]
        public bool Active { get; set; }

        public virtual RoleEntity Role { get; set; }

        //public override String ToString()
        //{
        //    Type objType = this.GetType();
        //    PropertyInfo[] propertyInfoList = objType.GetProperties(BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        //    StringBuilder result = new StringBuilder();
        //    result.AppendFormat(objType.Name + "[");
        //    bool flag = false;
        //    foreach (PropertyInfo propertyInfo in propertyInfoList)
        //    {
        //        result.AppendFormat("{0}={1}, ", propertyInfo.Name, propertyInfo.GetValue(this));
        //        flag = true;
        //    }
        //    if (flag)
        //        result.Remove(result.Length - 2, 1);
        //    result.AppendFormat("]");
        //    return result.ToString();
        //}

    }
}
