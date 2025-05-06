using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.DataContext.Dtos
{
    public class RegisterDto
    {

        [Required]
        public string Name { get; set; }
        [Required, EmailAddress(ErrorMessage = "The email address is not valid")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordConfirm { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int RoleId { get; set; }
        public int? RoomId { get; set; }

    }
}
