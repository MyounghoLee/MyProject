﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace biz.ConfigurationBiz
{
    public static class ConfigurationBuilderBiz
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static void SetConfgurationBuilder()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }
    }
}
