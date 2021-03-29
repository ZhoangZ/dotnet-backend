using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using System.Threading.Tasks;

namespace BackendDotnetCore.Enitities
{
    public class Product
    {
        public int Id { get; set; }
        public int SaleRate { get; set; }
        public string Name { get; set; }
        public virtual List<ImageProduct> Images { get; set; }



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
