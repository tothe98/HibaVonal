using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Services.Exceptions
{
    public class OrderWithIdNotExistsException : Exception
    {
        public OrderWithIdNotExistsException() : base("Order with this id not exists")
        {
        }
    }
}
