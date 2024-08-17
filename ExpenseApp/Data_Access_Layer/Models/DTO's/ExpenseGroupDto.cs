using Data_Access_Layer.helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models.DTO_s
{
    public class ExpenseGroupDto
    {
        public required string Group_Name { get; set; }
        public required string Description { get; set; }
        public List<string>? Members { get; set; }
        public double Expenses { get; set; } = 0.0;
    }
}
