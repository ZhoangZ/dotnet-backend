using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.Enitities
{
    public class ImageProduct
    {
        [JsonIgnore]
        public long Id { get; set; }
        [JsonIgnore]
        [Column("IMAGE")]
        public string _image;
        public string Image { 
            get {
                if (FileProcess.FileProcess.fileIsExists("product\\" + this._image))
                {
                    byte[] b = System.IO.File.ReadAllBytes(FileProcess.FileProcess.getFullPath("product\\"+this._image));
                    return "data:image/png;base64," + Convert.ToBase64String(b);
                }

                return this._image; 
            } 
            set { this._image = value; } }
      
        [JsonIgnore]
        public Product Product { get; set; }

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
