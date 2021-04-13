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


            //Cái này để truy cập api cho máy bên ngoài
            var hostUrl = configuration["hosturl"];
            if (string.IsNullOrEmpty(hostUrl)){
                //hostUrl = "https://192.168.0.111:5001";
                hostUrl = "https://25.50.183.23:80";
                // hostUrl = "https://192.168.2.217";
            }



            var host = new WebHostBuilder()
                .UseKestrel()
                 .ConfigureAppConfiguration((builderContext, config) =>
                 {
                     config.AddJsonFile("appsettings.json", optional: false);
                 })
                .UseUrls(hostUrl, "https://localhost:5001")   // <!-- this 
                //.UseUrls("https://localhost:5001")   // <!-- this 
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
