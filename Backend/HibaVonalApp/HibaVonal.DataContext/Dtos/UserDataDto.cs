using HibaVonal.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.DataContext.Dtos
{
    public  class UserDataDto
    {

        public string Email { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserRole> Roles { get; set; }

    }
}
