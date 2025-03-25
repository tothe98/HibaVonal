using Hibavonal.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HibaVonal.DataContext.Dtos
{
    public class OrderItemDto
    {
        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }

        [Required]
        public int OrderId { get; set; }
        [ForeignKey("OrderId"), JsonIgnore]
        public Order Order { get; set; }
    }
}
