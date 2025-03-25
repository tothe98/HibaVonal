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
    public class EquipmentDto
    {

        [Required]
        public string Name { get; set; }

        public int? ErrorTypeId { get; set; }
        [ForeignKey("ErrorTypeId")]
        public ErrorType? ErrorType { get; set; }
    }
}
