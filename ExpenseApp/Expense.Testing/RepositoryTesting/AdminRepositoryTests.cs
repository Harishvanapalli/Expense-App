using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Business_Layer.Repository.AdminRepository;
using Business_Layer.Repository.ExpenseRepository;
using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Testing.RepositoryTesting
{
    public class AdminRepositoryTests
    {

        private readonly Mock<ExpenseDbContext> _mockDbContext;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AdminInterface _adminRepository;

        public AdminRepositoryTests()
        {
            _mockDbContext = new Mock<ExpenseDbContext>();
            _mockMapper = new Mock<IMapper>();
            _adminRepository = new AdminInterface(_mockDbContext.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task CheckUserExistence_shouldReturnUserExistence_cannot_delete()
        {
            //Arrange
            var userId = "rameshupparapalli10@gmail.com";
            var expenseGroup = new ExpenseGroup
            {
                Group_Name = "Jalsa Trip Testing",
                Description = "Group for expenses in jalsa trip three",
                Members = new List<string> { "dileepthondupu8@gmail.com", "rameshupparapalli10@gmail.com" },
                Expenses = 29500.20
            };

            _mockDbContext.Setup(db => db.ExpenseGroups).ReturnsDbSet(new List<ExpenseGroup> { expenseGroup });

            //Act
            var response = await _adminRepository.CheckUserExistence(userId);

            //Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Can't Delete the User as user exists in expense groups", response.Message);

        }

        [Fact]
        public async Task CheckUserExistence_shouldReturnUserExistence_can_delete()
        {
            //Arrange
            var userId = "naveenbudha9@gmail.com";
            var expenseGroup = new ExpenseGroup
            {
                Group_Name = "Jalsa Trip Testing",
                Description = "Group for expenses in jalsa trip three",
                Members = new List<string> { "dileepthondupu8@gmail.com", "rameshupparapalli10@gmail.com" },
                Expenses = 29500.20
            };

            _mockDbContext.Setup(db => db.ExpenseGroups).ReturnsDbSet(new List<ExpenseGroup> { expenseGroup });

            //Act
            var response = await _adminRepository.CheckUserExistence(userId);

            //Assert
            Assert.True(response.IsSuccess);
            Assert.Equal("Can delete the User", response.Message);

        }


        [Fact]
        public async Task UpateExpenseDetails_shouldUpdateTheExpenseDetails()
        {
            //Arrange
            var UpdatedExpense = new Data_Access_Layer.Models.Expense
            {
                ExpenseID = 2,
                GroupId = 12,
                Description = "Jalsa Food Expense in Delhi",
                Amount = 5550.90,
                Paid_by = "rameshupparapalli10@gmail.com",
                Split_among = new List<string> { "dileepthondupu8@gmail.com", "rameshupparapalli10@gmail.com" },
                Paid_members = new List<string> { "dileepthondupu8@gmail.com", "rameshupparapalli10@gmail.com" },
                Issettled = true
            };


            var _mockDbSet = new Mock<DbSet<Data_Access_Layer.Models.Expense>>();
            _mockDbContext.Setup(db => db.Expenses).Returns(_mockDbSet.Object);

            //Act
            var result = await _adminRepository.UpdateExpenseDetails(UpdatedExpense);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Successfully updated the Expense", result.Message);
            _mockDbContext.Verify(db => db.Expenses.Update(It.IsAny<Data_Access_Layer.Models.Expense>()), Times.Once());
            _mockDbContext.Verify(db => db.SaveChangesAsync(default), Times.Once());
        }

        [Fact]
        public async Task GetUserDetails_shouldReturnUsers()
        {
            //Arrange 

            var users = new List<User> { new User { UserID = 1, Username = "sampleuser1@gmail.com", Password = "Sample1@123", Role = "Administrator" },
                                 new User { UserID = 2,  Username = "sampleuser2@gmail.com", Password = "Sample2@123", Role = "User" },
                                 new User { UserID = 3, Username = "sampleuser3@gmail.com", Password = "Sample3@123", Role = "User" }};

            var expectedUsers = new List<UserDto> { new UserDto { Username = "sampleuser2@gmail.com", Password = "Sample2@123" },
                                            new UserDto { Username = "sampleuser3@gmail.com", Password = "Sample3@123" }};

            _mockDbContext.Setup(db => db.Users).ReturnsDbSet(users);

            _mockMapper.Setup(m => m.Map<List<UserDto>>(It.IsAny<List<User>>())).Returns(expectedUsers);

            // Act
            var result = await _adminRepository.GetUserDetails();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Successfully Retrieved", result.Message);
            Assert.NotNull(result.Result);

            var userList = result.Result as List<UserDto>;
            Assert.NotNull(userList);
            Assert.Equal(2, userList.Count);

            Assert.Equal("sampleuser2@gmail.com", userList[0].Username);
        }

        [Fact]
        public async Task GetUserDetails_shouldReturnEmpty_UsersList()
        {
            //Arrange 

            var users = new List<User> { new User { UserID = 1, Username = "sampleuser1@gmail.com", Password = "Sample1@123", Role = "Administrator" },
                                 new User { UserID = 2,  Username = "sampleuser2@gmail.com", Password = "Sample2@123", Role = "Administrator" },
                                 new User { UserID = 3, Username = "sampleuser3@gmail.com", Password = "Sample3@123", Role = "Administrator" }};

            var expectedUsers = new List<UserDto> { };

            _mockDbContext.Setup(db => db.Users).ReturnsDbSet(users);

            _mockMapper.Setup(m => m.Map<List<UserDto>>(It.IsAny<List<User>>())).Returns(expectedUsers);

            // Act
            var result = await _adminRepository.GetUserDetails();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Successfully Retrieved", result.Message);
            Assert.NotNull(result.Result);

            var userList = result.Result as List<UserDto>;
            Assert.NotNull(userList);
            Assert.Empty(userList);
        }


        [Fact]
        public async Task DeleteUser_shouldDeleteTheUser()
        {
            //Arrange
            var user = new User
            {
                Username = "sampleuser10@gmail.com",
                Password = "Sample@123",
                Role = "User"
            };


            var userId = "sampleuser10@gmail.com";
            var users = new List<User> { user };

            _mockDbContext.Setup(db => db.Users).ReturnsDbSet(users);

            //Act
            var response = await _adminRepository.DeleteUser(userId);

            //Assert
            Assert.True(response.IsSuccess);
            Assert.Equal($"user with email: {userId} deleted Successfully", response.Message);
        }

        [Fact]
        public async Task DeleteUser_shouldReturnNull()
        {
            //Arrange
            var user = new User
            {
                Username = "sampleuser10@gmail.com",
                Password = "Sample@123",
                Role = "User"
            };


            var userId = "invaliduser@gmail.com";
            var users = new List<User> { user, new User { Username = "sampleuser12@gail.com", Password = "Sample2@123", Role = "User" } };

            _mockDbContext.Setup(db => db.Users).ReturnsDbSet(users);

            //Act
            var response = await _adminRepository.DeleteUser(userId);

            //Assert
            Assert.False(response.IsSuccess);
            Assert.Equal($"no user found with email: {userId}", response.Message);
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

            var ExceptionMessage = "Exception Occured while updating Expense";

            _mockDbContext.Setup(db => db.Expenses).Throws(new Exception(ExceptionMessage));

            // Act
            var result = await _adminRepository.UpdateExpenseDetails(expense);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal($"An error occurred {ExceptionMessage}", result.Message);
        }

        [Fact]
        public async Task GetUserDetails_shouldReturn_Excepion()
        {
            // Arrange
            var ExceptionMessage = "Some Exception Occurred while fetching user details";

            _mockDbContext.Setup(db => db.Users).Throws(new Exception(ExceptionMessage));

            //Act
            var result = await _adminRepository.GetUserDetails();

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal($"An error occurred {ExceptionMessage}", result.Message);
        }

        [Fact]
        public async Task CheckUserExistence_shouldReturn_Exception()
        {
            //Arrange
            var userID = "naveenbudha9@gmail.com";

            var ExceptionMessage = "Some Exception Occurred while checking user existence";

            _mockDbContext.Setup(db => db.ExpenseGroups).Throws(new Exception(ExceptionMessage));

            //Act
            var result = await _adminRepository.CheckUserExistence(userID);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal($"An error occurred {ExceptionMessage}", result.Message);
        }

        [Fact]
        public async Task DeleteUser_shouldReturn_Exception()
        {
            //Arrange
            var userID = "invaliduser@gmail.com";

            var ExceptionMessage = "Some Exception Occurred while deleting the user";

            _mockDbContext.Setup(x => x.Users).Throws(new Exception(ExceptionMessage));

            //Act
            var result = await _adminRepository.DeleteUser(userID);

            //ASsert
            Assert.False(result.IsSuccess);
            Assert.Equal($"An error occurred {ExceptionMessage}", result.Message);
        }

    }
}
