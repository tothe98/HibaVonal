using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Services.Exceptions
{
    public class RoomWithIdNotExistsException : Exception
    {
        public RoomWithIdNotExistsException() : base("Room with this id not exists")
        {
        }
    }
}
