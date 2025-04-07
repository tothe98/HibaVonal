using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Services.Exceptions
{
    public class EquipmentWithIdNotExistsException : Exception
    {
        public EquipmentWithIdNotExistsException() : base("Equipment with this id not exists")
        {
        }
    }
}
