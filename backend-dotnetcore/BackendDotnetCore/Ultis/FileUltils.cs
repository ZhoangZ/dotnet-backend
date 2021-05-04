using BackendDotnetCore.Entities;
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
        public static void setRequset(this List<Product2> lst, HttpRequest request)
        {
            lst.ForEach(delegate (Product2 p) {
                p.Images.ForEach(delegate (ImageProduct ip) {
                    ip.setRequest(request);
                });
            });
        }

        
    }
}
