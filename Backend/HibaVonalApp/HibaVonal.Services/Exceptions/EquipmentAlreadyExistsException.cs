using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Services.Exceptions
{
    public class EquipmentAlreadyExistsException : Exception
    {
        public EquipmentAlreadyExistsException() : base("Equipment already exists")
        {
        }
    }
}
