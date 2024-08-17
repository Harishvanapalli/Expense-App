using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO_s;
using Expense.API.Controllers;
using Expense.API.helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Testing.ControllerTesting
{
    public class AuthenticationControllerTesting
    {
        private readonly AuthenticationController _authController;
        private readonly Mock<JwtTokenHandlerClass> _mockjwtTokenHandler;
        private readonly Mock<ExpenseDbContext> _mockDbContext;
        public AuthenticationControllerTesting()
        {
            _mockDbContext = new Mock<ExpenseDbContext>();
            _mockjwtTokenHandler = new Mock<JwtTokenHandlerClass>(_mockDbContext.Object);
            _authController = new AuthenticationController(_mockjwtTokenHandler.Object);
        }

        [Fact]

        public void UserLogin_shouldAuthenticateUserLogin()
        {
            //Arrange

            var userDto = new UserDto { Username = "Sampleuser10@gmail.com", Password = "Sample@123" };

            var authenticateResponse = new AuthenticationResponse
            {
                Username = userDto.Username,
                Token = "sometoken",
                Expires_In = 30
            };

            _mockjwtTokenHandler.Setup(x => x.CreateToken(userDto)).Returns(authenticateResponse);

            //Act
            var response = _authController.UserLogin(userDto);

            //Assert
            Assert.IsType<ActionResult<AuthenticationResponse>>(response);
            Assert.Equal(userDto.Username, response.Value.Username);
            Assert.Equal("sometoken", response.Value.Token);
        }

        [Fact]
        public void UserLogin_shouldAuthenticateUserLogin_Invalid()
        {
            //Arrange

            var invalidUserDto = new UserDto { Username = "invaliduser@gmail.com", Password = "invalid@123" };

            _mockjwtTokenHandler.Setup(x => x.CreateToken(invalidUserDto)).Returns((AuthenticationResponse)null);

            //Act
            var response = _authController.UserLogin(invalidUserDto);

            //Assert
            Assert.IsType<UnauthorizedResult>(response.Result);
        }
    }
}
