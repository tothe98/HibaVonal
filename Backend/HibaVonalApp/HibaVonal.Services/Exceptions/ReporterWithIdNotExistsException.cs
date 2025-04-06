using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Services.Exceptions
{
    public class ReporterWithIdNotExistsException:Exception
    {
        public ReporterWithIdNotExistsException(string message) : base(message)
        {
        }
    }
}
