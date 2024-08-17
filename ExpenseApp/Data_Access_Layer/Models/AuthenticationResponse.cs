using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class AuthenticationResponse
    {
        public string Username { get; set; }

        public string Token { get; set; }

        public int Expires_In { get; set; }
    }
}
