using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Services.Exceptions
{
    public class DormitoryOnAddressAlreadyExistsException : Exception
    {
        public DormitoryOnAddressAlreadyExistsException() : base("Dormitory on this address already exists")
        {

        }
    }
}
