using Hibavonal.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.DataContext.Dtos
{
    public class SharedRoomDto : RoomDto
    {

        [Required]
        public string PersonInCharge { get; set; }

        [Required]
        public string PersonInChargeContact { get; set; }
    }
}
