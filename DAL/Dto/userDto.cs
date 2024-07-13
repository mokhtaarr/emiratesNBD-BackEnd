using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dto
{
    public class userDto
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
      
        public string Token { get; set; }

        public string MessageEn { get; set; }
        public bool statu { get; set; }
    }
}
