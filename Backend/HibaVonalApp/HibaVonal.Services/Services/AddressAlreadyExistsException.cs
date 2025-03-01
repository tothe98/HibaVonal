using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Services.Services
{
    public class AddressAlreadyExistsException : Exception
    {
        public AddressAlreadyExistsException() : base("Address already exists")
        {
        }
    }
}
