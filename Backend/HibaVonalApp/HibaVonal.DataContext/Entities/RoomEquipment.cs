using Hibavonal.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.DataContext.Entities
{
    public class RoomEquipment
    {
        [Key]
        public int RoomId { get; set; }
        public Room Room { get; set; }
        [Key]
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
        public int Quantity { get; set; }  
    }
}
