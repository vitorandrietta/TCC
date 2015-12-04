using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletronicos.Model
{
    public class ClientLogin
    {

        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ClientID { get; set; }

        public DateTime RegisterDate { get; set; }
        
    
    }
}
