using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using RefactorThis.Common;

namespace Domain.Product.Entities
{
    [Table("Product")]
    public class Product : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        public virtual ICollection<ProductOption> ProductOptions { get; set; }
    }
}
