using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Services.Exceptions
{
    public class AddressWithIdNotExistsException:Exception
    {
        public AddressWithIdNotExistsException() : base("Address does not exist with the given Id")
        {
        }

    }
}
