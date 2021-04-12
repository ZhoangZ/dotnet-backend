﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BackendDotnetCore.Enitities
{
    [Table("role")]
    public class RoleEntity
    {   
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Column("name")]
        public string Name { set; get; }

        [Column("description")]
        public string Description { set; get; }

        [Column("type")]
        public string Type { set; get; }

        [Column("created_by")]
        public string Created_by { set; get; }

        [Column("updated_by")]
        public string Update_by { set; get; }

        [Column("active")]
        public int Active { set; get; }

         public ICollection<UserRole> UserRoles { set; get; }

        public RoleEntity()
        {
           this.UserRoles = new List<UserRole>();
        }

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


    }
}
