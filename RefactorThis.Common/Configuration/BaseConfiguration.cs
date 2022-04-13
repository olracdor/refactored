using System;
using Microsoft.Extensions.Configuration;

namespace RefactorThis.Common.Configuration
{
    public class BaseConfiguration
    {
        private readonly IConfiguration _configuration;
        private readonly string _configPrefix;
        protected BaseConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            _configPrefix = "";
        }
        protected BaseConfiguration(string configPrefix, IConfiguration configuration)
        {
            _configuration = configuration;
            _configPrefix = configPrefix;
        }

        protected T GetSetting<T>(string settingName)
        {
            return GetSetting<T>(_configPrefix, settingName);
        }
        private T GetSetting<T>(string section, string settingName)
        {
            var key = string.IsNullOrEmpty(section) ? settingName : $"{section}:{settingName}";
            var rawValue = _configuration[key];
            if (string.IsNullOrEmpty(rawValue))
            {
                return default(T);
            }

            return (T)Convert.ChangeType(rawValue, typeof(T));

        }
    }
}
