using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RefactorThis.Common;
namespace Domain.Product.Repository
{
    public interface IProductRepository
    {
        ProductContext context { get; }
        Entities.Product Get(Guid Id);
        List<Entities.Product> Get();
        Entities.Product Save(Entities.Product entity);
        Entities.Product Update(Entities.Product entity);
        Entities.Product Delete(Entities.Product entity);
    }
}
