using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.DataContext.Dtos
{
    public class ErrorTypeDto
    {
        [Required]
        public string Name { get; set; }
    }
}
