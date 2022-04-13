using Autofac;
using RefactorThis.Providers;
using RefactorThis.Providers.Implementors;

namespace RefactorThis
{
    public class ProjectRegistrationModule : Module
    {
        /// <summary>
        /// Load the Project Dependancies
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductProvider>().As<IProductProvider>();
            builder.RegisterType<ProductOptionProvider>().As<IProductOptionProvider>();
            
        }
    }
}
