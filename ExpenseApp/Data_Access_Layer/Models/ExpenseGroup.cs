using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Data_Access_Layer.helpers;

namespace Data_Access_Layer.Models
{
    public class ExpenseGroup
    {
        [Key]
        public int ExpenseGroupID { get; set; }


        [Required(ErrorMessage = "Group Name is Required")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "The Length of Group name should be between ten to fifty characters")]
        public required string Group_Name { get; set; }


        [Required(ErrorMessage = "Should enter the Description")]
        [StringLength(50, MinimumLength = 15, ErrorMessage = "The length of Description should be between Fifteen to Fifty characters")]
        public required string Description { get; set; }


        public DateTime Created_Date { get; set; } = DateTime.Now;


        [CustomValidationAttributes(10)]
        public List<string>? Members { get; set; }



        [Range(0, double.MaxValue, ErrorMessage = "Expenses shoud be non negative number")]
        public double Expenses { get; set; } = 0.0;

        public ICollection<Expense> GroupExpenses { get; set; } = new List<Expense>();
    }
}
