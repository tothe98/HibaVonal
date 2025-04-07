using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Services.Exceptions
{
    public class DormitoryWithIdNotExistsException : Exception
    {
        public DormitoryWithIdNotExistsException() : base("Dormitory does not exist with the given Id")
        {
        }
    }
}
