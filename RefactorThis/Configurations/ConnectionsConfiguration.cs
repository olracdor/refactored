using Microsoft.Extensions.Configuration;
using RefactorThis.Common.Configuration;

namespace RefactorThis.Configurations
{
    public class ConnectionsConfiguration : BaseConfiguration
    {
        public ConnectionsConfiguration(IConfiguration configuration = null) : base("ConnectionStrings", configuration) { }
        public string ProductConnectionString => GetSetting<string>("Product");

    }
}
