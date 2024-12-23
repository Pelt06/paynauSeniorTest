using Backend.Domain.Entities.OrderDetail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Product
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        [Timestamp]
        public byte[] ? RowVersion { get; set; }

        public ICollection<OrderDetailEntity>? OrderDetail { get; set; }
    }
}
