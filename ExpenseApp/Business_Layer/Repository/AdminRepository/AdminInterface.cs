using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data_Access_Layer.Models.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace Business_Layer.Repository.AdminRepository
{
    public class AdminInterface : IAdminInterface
    {
        private readonly ExpenseDbContext _dbContext;

        private readonly IMapper _mapper;
        public AdminInterface(ExpenseDbContext _dbContext, IMapper _mapper)
        {
            this._mapper = _mapper;
            this._dbContext = _dbContext;
        }
        public async Task<ResponseClass> CheckUserExistence(string userId)
        {
            var response = new ResponseClass();
            try
            {
                var ExpenseGroups = await _dbContext.ExpenseGroups.FirstOrDefaultAsync(group => group.Members.Contains(userId));
                if (ExpenseGroups == null)
                {
                    response.Message = "Can delete the User";
                    response.IsSuccess = true;
                }
                else
                {
                    response.Message = "Can't Delete the User as user exists in expense groups";
                    response.IsSuccess = false;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred {ex.Message.ToString()}";
                response.IsSuccess = false;
                return response;
            }
        }

        public async Task<ResponseClass> DeleteUser(string userId)
        {
            var response = new ResponseClass();

            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Username == userId);

                if (user == null)
                {
                    response.Message = $"no user found with email: {userId}";
                    response.IsSuccess = false;
                }
                else
                {
                    _dbContext.Users.Remove(user);
                    _dbContext.SaveChanges();
                    response.Message = $"user with email: {userId} deleted Successfully";
                    response.IsSuccess = true;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred {ex.Message.ToString()}";
                response.IsSuccess = false;
                return response;
            }
        }

        public async Task<CommonResponse> GetUserDetails()
        {
            var response = new CommonResponse();
            try
            {
                List<User> users = await _dbContext.Users.Where(user => user.Role == "User").ToListAsync();
                response.Result = _mapper.Map<List<UserDto>>(users);
                response.Message = "Successfully Retrieved";
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred {ex.Message.ToString()}";
                response.IsSuccess = false;
                return response;
            }
        }

        public async Task<ResponseClass> UpdateExpenseDetails(Expense expense)
        {
            var response = new ResponseClass();

            try
            {
                _dbContext.Expenses.Update(expense);
                await _dbContext.SaveChangesAsync();
                response.Message = "Successfully updated the Expense";
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred {ex.Message.ToString()}";
                response.IsSuccess = false;
                return response;
            }
        }
    }
}
