using Hibavonal.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.DataContext.Dtos
{
    public class DormitoryDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Manager { get; set; }

        [Required]
        public string ManagerContact { get; set; }

        [Required]
        public int NumberOfFloors { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address? Address { get; set; }
    }
}
