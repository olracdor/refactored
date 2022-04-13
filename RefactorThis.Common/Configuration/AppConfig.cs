using System;
using Microsoft.Extensions.Configuration;

namespace RefactorThis.Common.Configuration
{
    public class AppConfig : BaseConfiguration
    {
        public AppConfig(IConfiguration configuration) : base(configuration) { }
       
    }
}
