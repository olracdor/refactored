using System;
using Microsoft.Extensions.Configuration;

namespace RefactorThis.Common.Configuration
{
    public class BasicAuthConfiguration : BaseConfiguration
    {
        public BasicAuthConfiguration(IConfiguration configuration) : base(configuration)
        {

        }
        /// <summary>
        /// Gets the Project Version from the configuration
        /// AppSettings: BasicAuth:IsEnabled
        /// </summary>
        public bool IsEnabled => GetSetting<bool>("BasicAuth.IsEnabled");

        /// <summary>
        /// Gets the Project Name from the configuration
        /// AppSetting: BasicAuth:Username
        /// </summary>
        public string Username => GetSetting<string>("BasicAuth.Username");

        /// <summary>
        /// Gets the Version from the configuration
        /// AppSettings: BasicAuth:Password
        /// </summary>
        public string Password => GetSetting<string>("BasicAuth.Password");
    }
}
