using HibaVonal.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.DataContext.Dtos
{
    public class PersonalRoomDto: RoomDto
    {
        [Required]
        public int Number { get; set; }

        public ICollection<User>? Residents { get; } = new List<User>();
    }
}
