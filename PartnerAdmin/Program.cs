using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using biz.CommonBiz;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace PartnerAdmin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CommonConfigurationBuilderBiz.SetConfgurationBuilder();

            ///Kestrel 웹서버 urls 설정 세팅
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseConfiguration(config) //Kestrel 웹서버 urls 설정 적용
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
