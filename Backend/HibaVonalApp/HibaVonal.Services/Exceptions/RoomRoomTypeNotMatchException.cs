using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Services.Exceptions
{
    public class RoomRoomTypeNotMatchException : Exception
    {
        public RoomRoomTypeNotMatchException() : base("The given room's RoomType does not match with the excpected type") 
        {

        }
    }
}
