using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class CommonResponse
    {
        public Object? Result { get; set; }

        public string? Message { get; set; }

        public Boolean IsSuccess { get; set; } = false;
    }
}
