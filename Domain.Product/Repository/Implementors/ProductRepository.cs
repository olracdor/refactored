using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Product.Repository.Implementors
{
    public class ProductRepository : IProductRepository
    {
        public ProductContext context { get; }
        public ProductRepository(ProductContext context)
        {
            this.context = context;
        }

        public Entities.Product Get(Guid Id)
        {
            return context.Product.Where(b => b.Id == Id)
                    .FirstOrDefault();
        }

        public List<Entities.Product> Get()
        {
            return context.Product.ToList();
        }

        public Entities.Product Save(Entities.Product entity)
        {
            context.Product.Add(entity);
            return entity;
        }

        public Entities.Product Update(Entities.Product entity)
        {
            context.Product.Update(entity);
            return entity;
        }

        public Entities.Product Delete(Entities.Product entity)
        {
            context.Product.Remove(entity);
            return entity;
        }
    }
}
