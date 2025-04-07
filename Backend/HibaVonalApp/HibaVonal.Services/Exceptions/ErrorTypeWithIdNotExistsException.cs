using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Services.Exceptions
{
    public class ErrorTypeWithIdNotExistsException : Exception
    {
        public ErrorTypeWithIdNotExistsException() : base("Error type with this id does not exists")
        {
            
        }
    }
}
