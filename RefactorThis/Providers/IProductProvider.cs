using System;
using System.Collections.Generic;
using RefactorThis.Dto;

namespace RefactorThis.Providers
{
    public interface IProductProvider
    {
        public List<ProductDto> Get();
        public ProductDto Get(Guid id);
        ProductDto Save(ProductDto entity);
        ProductDto Update(Guid id, ProductDto entity);
        void Delete(Guid id);
    }
}
