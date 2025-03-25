using Hibavonal.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.DataContext.Dtos
{
    public class OrderDto
    {
        public DateTime? Date { get; set; } = DateTime.Now;

        [Required]
        public int TotalAmount { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        public IList<OrderItem>? Items { get; set; } = new List<OrderItem>();
    }
}
