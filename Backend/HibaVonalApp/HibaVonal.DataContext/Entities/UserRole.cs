using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.DataContext.Entities
{
    public class UserRole
    {
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public User User { get; set; } = null!;
        
        [ForeignKey("RoleId")]
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}
