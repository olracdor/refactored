using Domain.Product;
using Microsoft.EntityFrameworkCore;
using RefactorThis.Test.TestData;
using System;
using RefactorThis.Common.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RefactorThis.Providers;
using RefactorThis.Providers.Implementors;
using Domain.Product.Repository.Implementors;

namespace RefactorThis.Test.Product
{
    public class ProductTests : BaseTestFixture<ProductContext>
    {
        IProductProvider _productProvider;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
           
            _productProvider = new ProductProvider(Mapper, new ProductRepository(Context));

        }

        [Test]
        public void TestPartsGetPartByCode()
        {

            Assert.IsNotNull(_productProvider.Get(Guid.Parse("31405b4f-58bc-4973-9970-a5cdcf707629")));
        }

        protected override void PopulateRepository()
        {
            Context.Product.Add(ProductsTestData.Product);
            Context.Product.Add(ProductsTestData.Product2);
        }

        protected override ProductContext SetupDbContext(DbContextOptions<ProductContext> option)
        {
            return new ProductContext(option);
        }
    }
}
