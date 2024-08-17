using AutoMapper;
using Business_Layer.Repository.ExpenseRepository;
using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace Expense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ExpenseController : ControllerBase
    {

        //private readonly ExpenseDbContext _dbContext;

        private readonly IExpenseRepository _expenseInterface;

        //private IMapper _mapper;
        public ExpenseController(IExpenseRepository _expenseInterface)
        {
            this._expenseInterface = _expenseInterface;
            //this._mapper = _mapper;
        }

        [HttpPost("addExpenseGroup")]

        [Authorize(Roles = "User")]
        public async Task<ActionResult<ResponseClass>> AddExpenseGroup([FromBody] ExpenseGroupDto expenseGroup)
        {
            var response = new ResponseClass();
            try
            {
                var FinalResult = await _expenseInterface.AddExpenseGroupMethod(expenseGroup);
                response.Message = FinalResult.Message;
                response.IsSuccess = FinalResult.IsSuccess;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred {ex.Message.ToString()}";
                response.IsSuccess = false;
                return response;
            }
        }

        [HttpPost("addExpense")]

        [Authorize(Roles = "User")]
        public async Task<ActionResult<ResponseClass>> AddExpense([FromBody] ExpenseDto expenseDto)
        {
            var response = new ResponseClass();
            try
            {
                var FinalResult = await _expenseInterface.AddExpenseMethod(expenseDto);
                response.Message = FinalResult.Message;
                response.IsSuccess = FinalResult.IsSuccess;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred {ex.Message.ToString()}";
                response.IsSuccess = false;
                return response;
            }
        }

        [HttpGet("getExpenseGroups/{userName}")]

        [Authorize(Roles = "User, Administrator")]

        public async Task<ActionResult<CommonResponse>> GetExpenseGroups(string userName)
        {
            var response = new CommonResponse();
            try
            {
                var FinalResult = await _expenseInterface.GetExpenseGroupsMethod(userName);
                response.Result = FinalResult.Result;
                response.Message = FinalResult.Message;
                response.IsSuccess = FinalResult.IsSuccess;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred {ex.Message.ToString()}";
                response.IsSuccess = false;
                return response;
            }

        }

        [HttpPut("updateUserPaid")]

        [Authorize(Roles = "User")]

        public async Task<ActionResult<ResponseClass>> UpdateUserPaidExpense([FromBody] Data_Access_Layer.Models.Expense expense)
        {
            var response = new ResponseClass();
            try
            {
                var FinalResult = await _expenseInterface.UpdateUserPaidExpenseMethod(expense);
                response.Message = FinalResult.Message;
                response.IsSuccess = FinalResult.IsSuccess;
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
