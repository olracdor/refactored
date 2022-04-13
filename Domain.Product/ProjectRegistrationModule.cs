using Autofac;
using Domain.Product.Repository;
using Domain.Product.Repository.Implementors;

namespace Domain.Product
{
    class ProjectRegistrationModule : Module
    {
        /// <summary>
        /// Load the Project Dependancies
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            builder.RegisterType<ProductOptionRepository>().As<IProductOptionRepository>();
        }
    }
}
