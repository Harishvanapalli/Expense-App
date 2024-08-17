using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repository.AdminRepository
{
    public interface IAdminInterface
    {
        Task<ResponseClass> UpdateExpenseDetails(Expense expense);

        Task<CommonResponse> GetUserDetails();

        Task<ResponseClass> CheckUserExistence(string userId);

        Task<ResponseClass> DeleteUser(string userId);
    }
}
