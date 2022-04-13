using System;
using System.Collections.Generic;

namespace Domain.Product.Repository
{
    public interface IProductOptionRepository
    {
        ProductContext context { get; }
        List<Entities.ProductOption> GetByProductId(Guid Id);
        List<Entities.ProductOption> Get(Guid Id);
        List<Entities.ProductOption> Get();
        Entities.ProductOption GetByProductIdAndId(Guid productId, Guid id);
        Entities.ProductOption Save(Entities.ProductOption entity);
        Entities.ProductOption Update(Entities.ProductOption entity);
        Entities.ProductOption Delete(Entities.ProductOption entity);
    }
}
