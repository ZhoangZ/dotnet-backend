using Microsoft.AspNetCore.Http;
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
    [Table("image_product")]
    public class ImageProduct
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [JsonIgnore]
        [Column("IMAGE")]
        public string _image;
        private HttpRequest request;
        public void setRequest(HttpRequest request)
        {
            this.request = request;
        }
        public string Image { 
            get {
                if (FileProcess.FileProcess.fileIsExists("product\\" + this._image))
                {
                    if (request == null)
                    {
                        //Base 64
                        byte[] b = System.IO.File.ReadAllBytes(FileProcess.FileProcess.getFullPath("product\\" + this._image));
                        return "data:image/png;base64," + Convert.ToBase64String(b);
                    }
                    else
                    {
                        string scheme = request.Scheme;
                        Microsoft.AspNetCore.Http.HostString host = request.Host;
                        string img=String.Format("{0}://{1}/resource/product/{2}", scheme, host.ToString(),this._image);
                        //Console.WriteLine(img);
                        return img;
                    }
                   
                }

                return this._image; 
            } 
            set { this._image = value; } }
      
        [JsonIgnore]
        public Product2 Product { get; set; }

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
