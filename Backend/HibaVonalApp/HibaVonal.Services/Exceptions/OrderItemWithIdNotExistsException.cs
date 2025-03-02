using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Services.Exceptions
{
    public class OrderItemWithIdNotExistsException : Exception
    {
        public OrderItemWithIdNotExistsException() : base("Order item with this id not exists")
        {
        }
    }
}
