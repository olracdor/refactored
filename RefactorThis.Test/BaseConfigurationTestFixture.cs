using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using RefactorThis.Common.Configuration;
using RefactorThis.Mappers;
using RefactorThis.Configurations;

namespace RefactorThis.Test
{
    [Parallelizable(ParallelScope.Fixtures)]
    public abstract class BaseConfigurationTestFixture
    {
        protected internal IMapper Mapper;
        public IConfiguration Configuration { get; set; }
        public AppConfig AppConfig { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetUpBase()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingDefinition());
            });
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json").Build();
            Mapper = mappingConfig.CreateMapper();
            AppConfig = new AppConfig(Configuration);
        }
    }
}
