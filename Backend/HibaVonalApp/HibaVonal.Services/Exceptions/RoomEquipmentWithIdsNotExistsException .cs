using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Services.Exceptions
{
    public class RoomEquipmentWithIdsNotExistsException : Exception
    {
        public RoomEquipmentWithIdsNotExistsException() : base("RoomEquipment with this ids not exists")
        {
        }
    }
}
