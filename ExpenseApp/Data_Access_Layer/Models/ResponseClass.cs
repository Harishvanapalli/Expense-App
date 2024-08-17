using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class ResponseClass
    {
        public string Message { get; set; }

        public Boolean IsSuccess { get; set; } = false;
    }
}
