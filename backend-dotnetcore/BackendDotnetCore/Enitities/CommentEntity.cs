﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.Enitities
{
    public class CommentEntity
    {
        [Key]
        [Column("id")]
        public int id { set; get; }

        [Column("content")]
        public string content { set; get; }

        [Column("active")]
        public int active { set; get; }

        [Column("product_id")]
        public int productID { set; get; }

        [JsonIgnore]
        public Product2 product { set; get; }

        [Column("user_id")]
        public int userID { set; get; }

        [JsonIgnore]
        public UserEntity user { set; get; }


        public override String ToString()
        {
            Type objType = this.GetType();
            PropertyInfo[] propertyInfoList = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
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