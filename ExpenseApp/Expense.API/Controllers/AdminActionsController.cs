using AutoMapper;
using Business_Layer.Repository.AdminRepository;
using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Expense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminActionsController : ControllerBase
    {
        private readonly IAdminInterface _adminInterface;  

        //private readonly IMapper _mapper;
        public AdminActionsController(IAdminInterface _adminInterface) {
            //this._mapper = _mapper;
            this._adminInterface = _adminInterface;
        }

        [HttpPut("updateExpense")]

        [Authorize(Roles = "Administrator")]

        public async Task<ActionResult<ResponseClass>> UpdateExpenseDetails([FromBody] Data_Access_Layer.Models.Expense expense)
        {
            var response = new ResponseClass();
            try
            {
                var FinalResult = await _adminInterface.UpdateExpenseDetails(expense);
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

        [HttpGet("getUsers")]

        [Authorize(Roles = "Administrator")]

        public async Task<ActionResult<CommonResponse>> GetUserDetails()
        {
            var response = new CommonResponse();
            try
            {
                var FinalResult = await _adminInterface.GetUserDetails();
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

        [HttpGet("checkUserExist/{userId}")]

        [Authorize(Roles = "Administrator")]

        public async Task<ActionResult<ResponseClass>> CheckUserExistence(string userId)
        {
            var response = new ResponseClass();
            try
            {
                var FinalResult = await _adminInterface.CheckUserExistence(userId);
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

        [HttpDelete("deleteUser/{userId}")]

        [Authorize(Roles = "Administrator")]

        public async Task<ActionResult<ResponseClass>> DeleteUser(string userId)
        {
            var response = new ResponseClass();
            try
            {
                var FinalResult = await _adminInterface.DeleteUser(userId);
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
