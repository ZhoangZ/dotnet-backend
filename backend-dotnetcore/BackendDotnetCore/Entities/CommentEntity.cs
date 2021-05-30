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
    public class CommentEntity
    {
        public int id { set; get; }
        public string content { set; get; }
        public int active { set; get; }

        public DateTime createdDate { set; get; }
        public double rate { set; get; }

        [JsonIgnore]
        public virtual UserEntity user { set; get; }
        public int userID { set; get; }

        [JsonIgnore]
        public virtual Product2 Product { set; get; }
        public int productID { set; get; }

     


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
