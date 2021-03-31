using BackendDotnetCore.Enitities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BackendDotnetCore.Ultis
{
    public static class FileUltils
    {
        public static void setRequset(this List<Product> lst, HttpRequest request)
        {
            lst.ForEach(delegate (Product p) {
                p.Images.ForEach(delegate (ImageProduct ip) {
                    ip.setRequest(request);
                });
            });
        }

        
    }
}
