using Business_Layer.Repository.AdminRepository;
using Business_Layer.Repository.ExpenseRepository;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO_s;
using Expense.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Testing.ControllerTesting
{
    public class AdminControllerTesting
    {
        private readonly Mock<IAdminInterface> _adminRepository;

        private readonly AdminActionsController _adminController;

        public AdminControllerTesting()
        {
            this._adminRepository = new Mock<IAdminInterface>();

            this._adminController = new AdminActionsController(_adminRepository.Object);
        }

        [Fact]
        public async Task UpdateExpenseDetails_ShouldUpdateExpenseDetails()
        {
            // Arrange
            var expense = new Data_Access_Layer.Models.Expense
            {
                ExpenseID = 3,
                GroupId = 12,
                Description = "Jalsa Cab Expense Delhi",
                Amount = 2400.90,
                Paid_by = "rameshupparapalli10@gmail.com",
                Split_among = new List<string> { "dileepthondupu8@gmail.com", "rameshupparapalli10@gmail.com" },
                Paid_members = new List<string> { "rameshupparapalli10@gmail.com" },
                Issettled = false
            };

            var expectedResponse = new ResponseClass
            {
                Message = "Successfully updated the Expense",
                IsSuccess = true
            };

            _adminRepository.Setup(x => x.UpdateExpenseDetails(It.IsAny<Data_Access_Layer.Models.Expense>()))
                              .ReturnsAsync(expectedResponse);

            // Act
            var result = await _adminController.UpdateExpenseDetails(expense);

            // Assert
            var okObjectResult = Assert.IsType<ActionResult<ResponseClass>>(result);
            var responseData = Assert.IsType<ResponseClass>(okObjectResult.Value);

            Assert.Equal(expectedResponse.Message, responseData.Message);
            Assert.Equal(expectedResponse.IsSuccess, responseData.IsSuccess);
        }

        [Fact]
        public async Task GetUserDetails_shouldReturnUserDetails()
        {
            // Arrange
            var Users = new List<User>
            {
                new User{Username = "testuser1@gmail.com", Password = "Testuser1@123", Role = "User"},
                new User {Username = "testuser2@gmail.com", Password = "Testuser2@123", Role = "User"}
            };

            var expectedResponse = new CommonResponse
            {
                Result = Users,
                Message = "Successfully Retrieved",
                IsSuccess = true
            };

            _adminRepository.Setup(x => x.GetUserDetails()).ReturnsAsync(expectedResponse);

            //Act
            var result = await _adminController.GetUserDetails();

            //Assert
            var okObjectResult = Assert.IsType<ActionResult<CommonResponse>>(result);
            var responseData = Assert.IsType<CommonResponse>(okObjectResult.Value);

            Assert.Equal(expectedResponse.Message, responseData.Message);
            Assert.Equal(expectedResponse.IsSuccess, responseData.IsSuccess);
            Assert.Equal(expectedResponse.Result, responseData.Result);
        }

        [Fact]
        public async Task CheckUserExistence_shouldReturnUserExistenceStatus_can_Delete()
        {
            //Arrangw
            var userID = "dileepthondupu8@gmail.com";

            var expectedResponse = new ResponseClass
            {
                Message = "Can delete the User",
                IsSuccess = true
            };

            _adminRepository.Setup(x => x.CheckUserExistence(It.IsAny<string>())).ReturnsAsync(expectedResponse);

            //Act
            var result = await _adminController.CheckUserExistence(userID);

            //Assert
            var okObjectResult = Assert.IsType<ActionResult<ResponseClass>>(result);
            var responseData = Assert.IsType<ResponseClass>(okObjectResult.Value);

            Assert.Equal(expectedResponse.Message, responseData.Message);
            Assert.Equal(expectedResponse.IsSuccess, responseData.IsSuccess);
        }

        [Fact]
        public async Task CheckUserExistence_shouldReturnUserExistenceStatus_cannot_delete()
        {
            //Arrangw
            var userID = "naveenbudha9@gmail.com";

            var expectedResponse = new ResponseClass
            {
                Message = "Can't Delete the User as user exists in expense groups",
                IsSuccess = false
            };

            _adminRepository.Setup(x => x.CheckUserExistence(It.IsAny<string>())).ReturnsAsync(expectedResponse);

            //Act
            var result = await _adminController.CheckUserExistence(userID);

            //Assert
            var okObjectResult = Assert.IsType<ActionResult<ResponseClass>>(result);
            var responseData = Assert.IsType<ResponseClass>(okObjectResult.Value);

            Assert.Equal(expectedResponse.Message, responseData.Message);
            Assert.Equal(expectedResponse.IsSuccess, responseData.IsSuccess);
        }

        [Fact]
        public async Task DeleteUser_shouldDeleteUser()
        {
            //Arrange
            var userID = "dileepthondupu8@gmail.com";
            var expectedResponse = new ResponseClass
            {
                Message = $"user with email: {userID} deleted Successfully",
                IsSuccess = true
            };

            _adminRepository.Setup(x => x.DeleteUser(It.IsAny<string>())).ReturnsAsync(expectedResponse);

            //Act
            var result = await _adminController.DeleteUser(userID);

            //ASsert
            var okObjectResult = Assert.IsType<ActionResult<ResponseClass>>(result);
            var responseData = Assert.IsType<ResponseClass>(okObjectResult.Value);

            Assert.Equal(expectedResponse.Message, responseData.Message);
            Assert.Equal(expectedResponse.IsSuccess, responseData.IsSuccess);
        }

        [Fact]
        public async Task DeleteUser_shouldReturnNoUserFound()
        {
            //Arrange
            var userID = "invaliduser@gmail.com";
            var expectedResponse = new ResponseClass
            {
                Message = $"no user found with email: {userID}",
                IsSuccess = false
            };

            _adminRepository.Setup(x => x.DeleteUser(It.IsAny<string>())).ReturnsAsync(expectedResponse);

            //Act
            var result = await _adminController.DeleteUser(userID);

            //ASsert
            var okObjectResult = Assert.IsType<ActionResult<ResponseClass>>(result);
            var responseData = Assert.IsType<ResponseClass>(okObjectResult.Value);

            Assert.Equal(expectedResponse.Message, responseData.Message);
            Assert.Equal(expectedResponse.IsSuccess, responseData.IsSuccess);
        }


        // Exception Test cases

        [Fact]
        public async Task UpdateExpenseDetails_ShouldReturn_Exception()
        {
            // Arrange
            var expense = new Data_Access_Layer.Models.Expense
            {
                ExpenseID = 3,
                GroupId = 12,
                Description = "Jalsa Cab Expense Delhi",
                Amount = 2400.90,
                Paid_by = "rameshupparapalli10@gmail.com",
                Split_among = new List<string> { "dileepthondupu8@gmail.com", "rameshupparapalli10@gmail.com" },
                Paid_members = new List<string> { "rameshupparapalli10@gmail.com" },
                Issettled = false
            };

            var ExceptionMessage = "Exceptio Occured while updating Expense";

            _adminRepository.Setup(x => x.UpdateExpenseDetails(expense)).ThrowsAsync(new Exception(ExceptionMessage));

            // Act
            var result = await _adminController.UpdateExpenseDetails(expense);

            // Assert
            var okObjectResult = Assert.IsType<ActionResult<ResponseClass>>(result);
            var responseData = Assert.IsType<ResponseClass>(okObjectResult.Value);

            Assert.False(responseData.IsSuccess);
            Assert.Equal($"An error occurred {ExceptionMessage}", responseData.Message);
        }

        [Fact]
        public async Task GetUserDetails_shouldReturn_Excepion()
        {
            // Arrange
            var ExceptionMessage = "Some Exception Occurred while fetching user details";

            _adminRepository.Setup(x => x.GetUserDetails()).ThrowsAsync(new Exception(ExceptionMessage));

            //Act
            var result = await _adminController.GetUserDetails();

            //Assert
            var okObjectResult = Assert.IsType<ActionResult<CommonResponse>>(result);
            var responseData = Assert.IsType<CommonResponse>(okObjectResult.Value);

            Assert.False(responseData.IsSuccess);
            Assert.Equal($"An error occurred {ExceptionMessage}", responseData.Message);
        }

        [Fact]
        public async Task CheckUserExistence_shouldReturn_Exception()
        {
            //Arrangw
            var userID = "naveenbudha9@gmail.com";

            var ExceptionMessage = "Some Exception Occurred while checking user existence";

            _adminRepository.Setup(x => x.CheckUserExistence(userID)).ThrowsAsync(new Exception(ExceptionMessage));

            //Act
            var result = await _adminController.CheckUserExistence(userID);

            //Assert
            var okObjectResult = Assert.IsType<ActionResult<ResponseClass>>(result);
            var responseData = Assert.IsType<ResponseClass>(okObjectResult.Value);

            Assert.False(responseData.IsSuccess);
            Assert.Equal($"An error occurred {ExceptionMessage}", responseData.Message);
        }

        [Fact]
        public async Task DeleteUser_shouldReturn_Exception()
        {
            //Arrange
            var userID = "invaliduser@gmail.com";

            var ExceptionMessage = "Some Exception Occurred while deleting the user";

            _adminRepository.Setup(x => x.DeleteUser(userID)).ThrowsAsync(new Exception(ExceptionMessage));

            //Act
            var result = await _adminController.DeleteUser(userID);

            //ASsert
            var okObjectResult = Assert.IsType<ActionResult<ResponseClass>>(result);
            var responseData = Assert.IsType<ResponseClass>(okObjectResult.Value);

            Assert.False(responseData.IsSuccess);
            Assert.Equal($"An error occurred {ExceptionMessage}", responseData.Message);
        }
    }
}
