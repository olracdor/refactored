using System;
using Microsoft.Extensions.DependencyInjection;

namespace RefactorThis.Mappers
{
 
    public static class AutoMappingSetup
    {
        public static void AutoMapperConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingDefinition));
        }

    }
    
}
