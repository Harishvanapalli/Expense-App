using AutoMapper;
using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO_s;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repository.ExpenseRepository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ExpenseDbContext _dbContext;

        private readonly IMapper _mapper;
        public ExpenseRepository(ExpenseDbContext _dbContext, IMapper _mapper) {
            this._dbContext = _dbContext;
            this._mapper = _mapper;
        }
        public async Task<ResponseClass> AddExpenseGroupMethod(ExpenseGroupDto expenseGroup)
        {
            var response = new ResponseClass();

            try
            {
                var group = _mapper.Map<ExpenseGroup>(expenseGroup);

                // Validate the model state
                var validationResults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(group, new ValidationContext(group), validationResults, true);

                if (!isValid)
                {
                    //response.Message = "There are some validation errors: " + string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)); 
                    response.Message = "There are some validation errors in expenseGroup properties";
                    response.IsSuccess = false;
                    return response;
                }

                _dbContext.ExpenseGroups.Add(group);
                await _dbContext.SaveChangesAsync();

                response.Message = "Expense Group Created Successfully";
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message}";
                response.IsSuccess = false;
                return response;
            }
        }

        public async Task<ResponseClass> AddExpenseMethod(ExpenseDto expenseDto)
        {
            var response = new ResponseClass();

            try
            {
                Expense expense = _mapper.Map<Expense>(expenseDto);

                expense.Paid_members.Add(expense.Paid_by);

                //Validate the model state
                var validationResults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(expense, new ValidationContext(expense), validationResults, true);

                if (!isValid)
                {
                    //response.Message = "There are some validation errors: " + string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                    response.Message = "There are some validation errors in Expense properties";
                    response.IsSuccess = false;
                    return response;
                }

                _dbContext.Expenses.Add(expense);
                await _dbContext.SaveChangesAsync();

                response.Message = "Expense Added Successfully";
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message}";
                response.IsSuccess = false;
                return response;
            }
        }

        public async Task<CommonResponse> GetExpenseGroupsMethod(string userName)
        {
            var response = new CommonResponse();

            try
            {
                var user = await _dbContext.Users.Where(u => u.Username == userName).FirstOrDefaultAsync();
                List<ExpenseGroup> Groups;
                if(user?.Role == "User") {
                    Groups = await _dbContext.ExpenseGroups.Where(x => x.Members.Contains(userName)).ToListAsync();
                }
                else
                {
                    Groups = await _dbContext.ExpenseGroups.ToListAsync();
                }

                foreach (var group in Groups)
                {
                    group.GroupExpenses = await _dbContext.Expenses.Where(e => e.GroupId == group.ExpenseGroupID).ToListAsync();
                }

                List<ExpensesGroupResponseDto> responseGroups = _mapper.Map<List<ExpensesGroupResponseDto>>(Groups);

                response.Result = responseGroups;
                response.Message = "Successfully Retrieved";
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                response.IsSuccess = false;
                return response;
            }
        }

        public async Task<ResponseClass> UpdateUserPaidExpenseMethod(Expense expense)
        {
            var response = new ResponseClass();

            try
            {
                _dbContext.Expenses.Update(expense);
                await _dbContext.SaveChangesAsync();
                response.Message = "Successfully Paid the Expense";
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                response.IsSuccess = false;
                return response;
            }
        }
    }
}
