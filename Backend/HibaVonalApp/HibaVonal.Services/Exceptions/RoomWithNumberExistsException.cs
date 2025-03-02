using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Services.Exceptions
{
    public class RoomWithNumberExistsException : Exception
    {
        public RoomWithNumberExistsException() : base("Room with this number already exists")
        {
        }
    }
}
