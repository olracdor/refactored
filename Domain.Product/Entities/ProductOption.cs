using System;
using System.ComponentModel.DataAnnotations.Schema;
using RefactorThis.Common;

namespace Domain.Product.Entities
{
    [Table("ProductOption")]
    public class ProductOption : IEntity
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        
        public virtual Product Product { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
