using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Data_Access_Layer.helpers;

namespace Data_Access_Layer.Models
{
    public class Expense
    {
        [Key]
        public int ExpenseID { get; set; }

        public int GroupId { get; set; }

        [Required(ErrorMessage ="Description is Required!")]
        [StringLength(50, MinimumLength = 15, ErrorMessage ="The length of Description should be between Fifteen to Fifty characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage ="Amount is Required!")]
        [Range(0, double.MaxValue, ErrorMessage ="The Amount must be a positive number")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Paid By is required!")]
        [EmailAddress(ErrorMessage = "SHould enter the email of user who paid this expense")]
        public string? Paid_by { get; set; }

        public DateTime Created_date { get; set; } = DateTime.Now;

        [SpliAmongValidation]
        public List<string>? Split_among { get; set; }

        public List<string>? Paid_members { get; set; } = new List<string>();

        public Boolean Issettled { get; set; } = false;
    }
}
