using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BackendDotnetCore.Enitities
{
    public class RoleEntity
    {   
        [Column("id")]
        public int Id;
        [Column("name")]
        public string Name;

        [Column("description")]
        public string Description;

        [Column("type")]
        public string Type;

        [Column("created_by")]
        public string Created_by;

        [Column("updated_by")]
        public string Update_by;

        [Column("active")]
        public int Active;


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
