using Data_Access_Layer.helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models.DTO_s
{
    public class ExpenseDto
    {
        public int GroupId { get; set; }
        public string? Description { get; set; }
        public double Amount { get; set; }
        public string? Paid_by { get; set; }
        public List<string>? Split_among { get; set; }
    }
}
