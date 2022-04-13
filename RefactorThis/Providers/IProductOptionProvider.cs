using System;
using System.Collections.Generic;
using Domain.Product.Entities;
using RefactorThis.Dto;

namespace RefactorThis.Providers
{
    public interface IProductOptionProvider
    {
        List<ProductOptionDto> GetByProductId(Guid Id);
        List<ProductOptionDto> Get(Guid Id);
        List<ProductOptionDto> Get();
        ProductOptionDto GetByProductIdAndId(Guid productId, Guid id);
        ProductOptionDto Save(Guid id, ProductOptionDto entity);
        ProductOptionDto Update(Guid productid, Guid id, ProductOptionDto entity);
        void Delete(Guid productId, Guid id);
    }
}
