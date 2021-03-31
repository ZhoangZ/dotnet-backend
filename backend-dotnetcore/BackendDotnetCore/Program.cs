using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using BackendDotnetCore.DAO;
using BackendDotnetCore.Enitities;
using System.Text;
using BackendDotnetCore.EF;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace BackendDotnetCore
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;



            /*
             //Product a= new ProductDAO().getAccount(1);
             Product a= new ProductDAO().getProduct(1);
            // new BackendDotnetDbContext().Products.Include
             Console.WriteLine(a);
             Console.WriteLine(a.Images.Count);*/

            // use this to allow command line parameters in the config
            var configuration = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();


            var hostUrl = configuration["hosturl"];
            if (string.IsNullOrEmpty(hostUrl))
                hostUrl = "https://192.168.0.111:5001";


            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(hostUrl, "https://localhost:5001")   // <!-- this 
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseConfiguration(configuration)
                .Build();

            host.Run();



            //CreateHostBuilder(args).Build().Run();
            }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseStartup<Startup>();
                 });
       
    }
}
