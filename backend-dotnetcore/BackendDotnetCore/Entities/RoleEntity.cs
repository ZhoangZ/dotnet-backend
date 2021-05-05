using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.Entities
{
    //[Table("role")]
    public class RoleEntity
    {   
        
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Column("name")]
        public string Name { set; get; }

        [Column("description")]
        public string Description { set; get; }

        [Column("type")]
        public string Type { set; get; }

        [Column("created_by")]
        public int Created_by { set; get; }

        //public virtual UserEntity userCreate { set; get; }
        [Column("updated_by")]
        public int Update_by { set; get; }

        //public virtual UserEntity userUpdate { set; get; }

        [Column("active")]
        public int Active { set; get; }
       
        [JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; }




        public bool checkRoleInfo()
        {
            //checkSomeFields of role
            if(this.Name == null || this.Name == "" || this.Name == " ")
            {
                return false;
            }
            else
            {
                return true;
            }
        }




        public override String ToString()
        {
            Type objType = this.GetType();
            PropertyInfo[] propertyInfoList = objType.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
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
