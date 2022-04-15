using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Product.Entities;

namespace Domain.Product.Repository.Implementors
{
    public class ProductOptionRepository : IProductOptionRepository
    {
        public ProductOptionRepository(ProductContext context)
        {
            this.context = context;
        }

        public ProductContext context { get; }

        public List<ProductOption> Get()
        {
            return context.ProductOption.ToList();
        }

        public ProductOption Save(ProductOption entity)
        {
            context.ProductOption.Add(entity);
            return entity;
        }

        public ProductOption Update(ProductOption entity)
        {
            context.ProductOption.Update(entity);
            return entity;
        }

        public ProductOption Delete(ProductOption entity)
        {
            context.ProductOption.Remove(entity);
            return entity;
        }

        public List<ProductOption> GetByProductId(Guid productId)
        {
            return context.ProductOption.Where(b => b.ProductId == productId).ToList();
        }


        public ProductOption GetByProductIdAndId(Guid productId, Guid id)
        {
            return context.ProductOption.Where(b => b.Id == id && b.ProductId == productId)
                    .FirstOrDefault();
        }

        public List<ProductOption> Get(Guid Id)
        {
            return context.ProductOption.Where(b => b.Id == Id).ToList();
        }
    }
}
