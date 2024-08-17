using Data_Access_Layer.Models.DTO_s;
using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Business_Layer.Repository.ExpenseRepository
{
    public interface IExpenseRepository
    {
        Task<ResponseClass> AddExpenseGroupMethod(ExpenseGroupDto expenseGroup);

        Task<ResponseClass> AddExpenseMethod(ExpenseDto expenseDto);

        Task<CommonResponse> GetExpenseGroupsMethod(string userName);

        Task<ResponseClass> UpdateUserPaidExpenseMethod(Expense expense);
    }
}
