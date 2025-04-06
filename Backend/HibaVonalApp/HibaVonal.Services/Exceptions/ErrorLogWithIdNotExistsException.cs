using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Services.Exceptions
{
    public class ErrorLogWithIdNotExistsException:Exception
    {
        public ErrorLogWithIdNotExistsException(string message) : base(message)
        {
        }
        public ErrorLogWithIdNotExistsException() : base("Error log with the given id does not exist")
        {
        }
    }
}
