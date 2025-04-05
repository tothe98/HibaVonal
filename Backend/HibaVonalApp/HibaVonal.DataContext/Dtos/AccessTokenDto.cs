using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.DataContext.Dtos
{
    public class AccessTokenDto
    {
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
        public UserDataDto UserData { get; set; }
    }
}
