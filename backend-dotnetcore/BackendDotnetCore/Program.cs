using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using BackendDotnetCore.DAO;
using BackendDotnetCore.Enitities;
using System.Text;
using BackendDotnetCore.EF;


namespace BackendDotnetCore
{
    class Program
    {
        static void Main(string[] args)
        {

           /* Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            //Product a= new ProductDAO().getAccount(1);
            Product a= new ProductDAO().getProduct(1);
           // new BackendDotnetDbContext().Products.Include
            Console.WriteLine(a);
            Console.WriteLine(a.Images.Count);*/
           




                CreateHostBuilder(args).Build().Run();
            }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseStartup<Startup>();
                 });
       
    }
}
