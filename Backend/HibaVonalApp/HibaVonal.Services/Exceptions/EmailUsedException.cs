using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Services.Exceptions
{
    public class EmailUsedException : Exception
    {
        public EmailUsedException() : base("The given email is already used!")
        {
        }
    }
}
