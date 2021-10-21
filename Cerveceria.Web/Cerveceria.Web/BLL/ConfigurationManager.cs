using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Cerveceria.Web.BLL
{
    public class ConfigurationManager
    {
        private static IConfiguration _configuration;
        public ConfigurationManager(string path)
        {
            
            _configuration = new ConfigurationBuilder()
            .AddJsonFile(path, true, true)
            .Build();

        }
        public T GetValue<T>(string configSection, string keyName)
        {
            return (T)Convert.ChangeType(_configuration[$"{configSection}:{keyName}"], typeof(T));
        }
        private T GetValue<T>(string configSection, string configSubSection, string keyName)
        {
            return (T)Convert.ChangeType(_configuration[$"{configSection}:{configSubSection}:{keyName}"], typeof(T));
        }

    }
}
