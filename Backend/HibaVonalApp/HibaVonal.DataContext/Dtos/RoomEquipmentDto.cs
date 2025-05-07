using Hibavonal.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.DataContext.Dtos
{
    public class RoomEquipmentDto
    {
        public int RoomId { get; set; }
        public RoomDto Room { get; set; }
        public int EquipmentId { get; set; }
        public EquipmentDto Equipment { get; set; }
        public int Quantity { get; set; }
    }

    public class RoomEquipmentCreateDto
    {
        public int RoomId { get; set; }
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }
    }
    public class RoomEquipmentUpdateDto
    {
        
        public int Quantity { get; set; }
    }

    
}
