using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.Configuration.Json;

namespace DanielMarques.Utilities
{
    internal static class Resources
    {
        internal static IConfigurationRoot Configuration { get; set; }

        internal static string GetString(string file, string key)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($".\\strings\\{file}.json");

            IConfigurationRoot configuration = builder.Build();

            return configuration[key];
        }
    }
}