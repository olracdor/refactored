using System;
namespace RefactorThis.Dto
{
    public class ProductOptionDto
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public ProductDto Product { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
