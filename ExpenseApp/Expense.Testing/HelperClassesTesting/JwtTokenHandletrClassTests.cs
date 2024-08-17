using Data_Access_Layer.Models.DTO_s;
using Data_Access_Layer.Models;
using Expense.API.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer.Data;
using Moq;
using Moq.EntityFrameworkCore;

namespace Expense.Testing.HelperClassesTesting
{
    public class JwtTokenHandletrClassTests
    {
        private readonly JwtTokenHandlerClass _jwtTokenHandlerClass;
        private readonly Mock<ExpenseDbContext> _mockDbContext;

        public JwtTokenHandletrClassTests()
        {
            _mockDbContext = new Mock<ExpenseDbContext>();
            _jwtTokenHandlerClass = new JwtTokenHandlerClass(_mockDbContext.Object);
        }

        [Fact]
        public void CreateToken_shouldReturnToken_validUser()
        {
            //Arrange
            var _jwtTokenHandlerClass = new JwtTokenHandlerClass(_mockDbContext.Object);

            var users = new List<User> { new User { UserID = 1, Username = "sampleuser1@gmail.com", Password = "Sample1@123", Role = "Administrator" },
                                 new User { UserID = 2,  Username = "sampleuser2@gmail.com", Password = "Sample2@123", Role = "User" },
                                 new User { UserID = 3, Username = "sampleuser3@gmail.com", Password = "Sample3@123", Role = "User" }};

            var userDto = new UserDto { Username = "sampleuser1@gmail.com", Password = "Sample1@123" };

            _mockDbContext.Setup(db => db.Users).ReturnsDbSet(users);

            //Act
            var result = _jwtTokenHandlerClass.CreateToken(userDto);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Username, userDto.Username);
            Assert.NotEmpty(result.Token);
            Assert.True(result.Expires_In > 0);

        }

        [Fact]
        public void CreateToken_shouldReturnNull_InValidUser()
        {
            //Arrange
            var _jwtTokenHandlerClass = new JwtTokenHandlerClass(_mockDbContext.Object);

            var users = new List<User> { new User { UserID = 1, Username = "sampleuser1@gmail.com", Password = "Sample1@123", Role = "Administrator" },
                                 new User { UserID = 2,  Username = "sampleuser2@gmail.com", Password = "Sample2@123", Role = "User" },
                                 new User { UserID = 3, Username = "sampleuser3@gmail.com", Password = "Sample3@123", Role = "User" }};

            var userDto = new UserDto { Username = "Invaliduser1@gmail.com", Password = "Invalid@123" };

            _mockDbContext.Setup(db => db.Users).ReturnsDbSet(users);

            //Act
            var result = _jwtTokenHandlerClass.CreateToken(userDto);

            //Assert
            Assert.Null(result);

        }
    }
}
