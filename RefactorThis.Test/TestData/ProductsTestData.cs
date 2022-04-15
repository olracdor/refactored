using System;
namespace RefactorThis.Test.TestData
{
    public static class ProductsTestData
    {
        public static Domain.Product.Entities.Product Product => new Domain.Product.Entities.Product
        {
            Id = Guid.Parse("31405b4f-58bc-4973-9970-a5cdcf707629"),

            Name = "Test Product",

            Description = "Test Product",

            Price = 10.99m
        };

        public static Domain.Product.Entities.Product Product2 => new Domain.Product.Entities.Product
        {
            Id = Guid.Parse("31405b4f-58bc-4973-9970-a5cdcf707621"),

            Name = "Test Product 2",

            Description = "Test Product 2",

            Price = 11.99m
        };
    }
}
