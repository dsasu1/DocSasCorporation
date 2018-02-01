using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;
namespace DSCAppEssentials.Managers
{
    /// <summary>
    /// Class ConfigManager.
    /// </summary>
    public class ConfigManager
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        public static string GetConnectionString(IConfiguration config, string key)
        {
            if (config == null)
            {
                config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
            }

            return config.GetConnectionString(key);
            
        }
    }
}
