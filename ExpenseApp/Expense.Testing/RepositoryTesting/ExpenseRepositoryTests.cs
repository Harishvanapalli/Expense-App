using AutoMapper;
using Business_Layer.Repository.ExpenseRepository;
using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO_s;
using Expense.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Testing.RepositoryTesting
{
    public class ExpenseRepositoryTests
    {
        private readonly Mock<ExpenseDbContext> _mockDbContext;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ExpenseRepository _expenseRepository;

        public ExpenseRepositoryTests()
        {
            _mockDbContext = new Mock<ExpenseDbContext>();
            _mockMapper = new Mock<IMapper>();
            _expenseRepository = new ExpenseRepository(_mockDbContext.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task AddExpenseGroup_shouldAddExpenseGroup()
        {
            //Arrange

            var expenseGroupDto = new ExpenseGroupDto
            {
                Group_Name = "Jalsa Trip Three",
                Description = "Group for expenses in jalsa trip three",
                Members = new List<string> { "dileepthondupu8@gmail.com", "rameshupparapalli10@gmail.com" },
                Expenses = 29500.20
            };

            var expectedExpenseGroup = new ExpenseGroup
            {
                Group_Name = expenseGroupDto.Group_Name,
                Description = expenseGroupDto.Description,
                Members = expenseGroupDto.Members,
                Expenses = expenseGroupDto.Expenses
            };

            _mockMapper.Setup(m => m.Map<ExpenseGroup>(expenseGroupDto)).Returns(expectedExpenseGroup);

            var _mockDbSet = new Mock<DbSet<ExpenseGroup>>();
            _mockDbContext.Setup(db => db.ExpenseGroups).Returns(_mockDbSet.Object);

            // Act
            var result = await _expenseRepository.AddExpenseGroupMethod(expenseGroupDto);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Expense Group Created Successfully", result.Message);
            _mockDbContext.Verify(db => db.ExpenseGroups.Add(It.IsAny<ExpenseGroup>()), Times.Once);
            _mockDbContext.Verify(db => db.SaveChangesAsync(default), Times.Once);

        }


        [Fact]
        public async Task AddExpenseGroup_shouldReturn_ValidationErrors()
        {
            //Arrange

            var expenseGroupDto = new ExpenseGroupDto
            {
                Group_Name = "Jalsa",
                Description = "invalid",
                Members = new List<string> { "dileepthondupu8@gmail.com", "rameshupparapalli10@gmail.com" },
                Expenses = 29500.20
            };

            var expectedExpenseGroup = new ExpenseGroup
            {
                Group_Name = expenseGroupDto.Group_Name,
                Description = expenseGroupDto.Description,
                Members = expenseGroupDto.Members,
                Expenses = expenseGroupDto.Expenses
            };

            _mockMapper.Setup(m => m.Map<ExpenseGroup>(expenseGroupDto)).Returns(expectedExpenseGroup);

            var _mockDbSet = new Mock<DbSet<ExpenseGroup>>();
            _mockDbContext.Setup(db => db.ExpenseGroups).Returns(_mockDbSet.Object);

            // Act
            var result = await _expenseRepository.AddExpenseGroupMethod(expenseGroupDto);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("There are some validation errors in expenseGroup properties", result.Message);

        }

        [Fact]
        public async Task AddExpense_shouldAddExpense()
        {
            // Arrange
            var addExpenseDto = new ExpenseDto
            {
                GroupId = 12,
                Description = "Jalsa 2024 Travell Expenses",
                Amount = 12500.90,
                Paid_by = "dileepthondupu8@gmail.com",
                Split_among = new List<string> { "dileepthondupu8@gmail.com", "rameshupparapalli10@gmail.com" }
            };

            var ExpectedExpense = new Data_Access_Layer.Models.Expense
            {
                GroupId = addExpenseDto.GroupId,
                Description = addExpenseDto.Description,
                Amount = addExpenseDto.Amount,
                Paid_by = addExpenseDto.Paid_by,
                Split_among = addExpenseDto.Split_among
            };

            _mockMapper.Setup(m => m.Map<Data_Access_Layer.Models.Expense>(addExpenseDto)).Returns(ExpectedExpense);

            var _mockDbSet = new Mock<DbSet<Data_Access_Layer.Models.Expense>>();
            _mockDbContext.Setup(db => db.Expenses).Returns(_mockDbSet.Object);
           

            //Act
            var result = await _expenseRepository.AddExpenseMethod(addExpenseDto);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Expense Added Successfully", result.Message);
            _mockDbContext.Verify(db => db.Expenses.Add(It.IsAny<Data_Access_Layer.Models.Expense>()), Times.Once());
            _mockDbContext.Verify(db => db.SaveChangesAsync(default), Times.Once());
        }

        [Fact]
        public async Task AddExpense_shouldReturn_ValidationErrors()
        {
            // Arrange
            var addExpenseDto = new ExpenseDto
            {
                GroupId = 12,
                Description = "Invalid",
                Amount = 12500.90,
                Paid_by = "dileepthondupu8@gmail.com",
                Split_among = new List<string> { "dileepthondupu8@gmail.com", "rameshupparapalli10@gmail.com" }
            };

            var ExpectedExpense = new Data_Access_Layer.Models.Expense
            {
                GroupId = addExpenseDto.GroupId,
                Description = addExpenseDto.Description,
                Amount = addExpenseDto.Amount,
                Paid_by = addExpenseDto.Paid_by,
                Split_among = addExpenseDto.Split_among
            };

            _mockMapper.Setup(m => m.Map<Data_Access_Layer.Models.Expense>(addExpenseDto)).Returns(ExpectedExpense);

            var _mockDbSet = new Mock<DbSet<Data_Access_Layer.Models.Expense>>();
            _mockDbContext.Setup(db => db.Expenses).Returns(_mockDbSet.Object);


            //Act
            var result = await _expenseRepository.AddExpenseMethod(addExpenseDto);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("There are some validation errors in Expense properties", result.Message);
        }

        [Fact]
        public async Task UpdateUserPaidExpense_shouldUpdateExpense()
        {
            //Arrange
            var ExpensetoUpdate = new Data_Access_Layer.Models.Expense
            {
                ExpenseID = 2,
                GroupId = 12,
                Description = "Jalsa Food Expense Delhi",
                Amount = 5400.90,
                Paid_by = "rameshupparapalli10@gmail.com",
                Split_among = new List<string> { "dileepthondupu8@gmail.com", "rameshupparapalli10@gmail.com" },
                Paid_members = new List<string> { "rameshupparapalli10@gmail.com" },
                Issettled = false
            };

            var _mockDbSet = new Mock<DbSet<Data_Access_Layer.Models.Expense>>();
            _mockDbContext.Setup(db => db.Expenses).Returns(_mockDbSet.Object);

            //Act
            var result = await _expenseRepository.UpdateUserPaidExpenseMethod(ExpensetoUpdate);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Successfully Paid the Expense", result.Message);
            _mockDbContext.Verify(db => db.Expenses.Update(It.IsAny<Data_Access_Layer.Models.Expense>()), Times.Once());
            _mockDbContext.Verify(db => db.SaveChangesAsync(default), Times.Once());
        }

        [Fact]
        public async Task GetExpenseGroupsMethod_ShouldReturnGroups()
        {
            // Arrange
            var userName = "sampleuser10@gmail.com";
            var user = new User { Username = userName, Role = "User" };
            var users = new List<User> { user };

            var groups = new List<ExpenseGroup>
            {
                new ExpenseGroup
                {
                    ExpenseGroupID = 1,
                    Group_Name = "Test Group",
                    Description = "Test Group Description",
                    Created_Date = DateTime.Now,
                    Members = new List<string> { userName },
                    Expenses = 5000.0,
                    GroupExpenses = new List<Data_Access_Layer.Models.Expense>()
                }
            };

            var expenses = new List<Data_Access_Layer.Models.Expense>
            {
                new Data_Access_Layer.Models.Expense
                {
                    ExpenseID = 1,
                    GroupId = 1,
                    Description = "Test Expense",
                    Amount = 1000.0,
                    Paid_by = userName,
                    Created_date = DateTime.Now,
                    Split_among = new List<string> { userName },
                    Paid_members = new List<string> { userName },
                    Issettled = false
                }
            };

            _mockDbContext.Setup(db => db.Users).ReturnsDbSet(users);

            _mockDbContext.Setup(db => db.ExpenseGroups).ReturnsDbSet(groups);
          
            _mockDbContext.Setup(db => db.Expenses).ReturnsDbSet(expenses);

            // will mock Mapper
            var expenseGroupResponseDtos = new List<ExpensesGroupResponseDto>
            {
                new ExpensesGroupResponseDto
                {
                    ExpenseGroupID = 1,
                    Group_Name = "Test Group",
                    Description = "Test Group Description",
                    Created_Date = DateTime.Now,
                    Members = new List<string> { userName },
                    Expenses = 5000.0,
                    GroupExpenses = expenses
                }
            };

            _mockMapper.Setup(m => m.Map<List<ExpensesGroupResponseDto>>(groups)).Returns(expenseGroupResponseDtos);

            // Act
            var result = await _expenseRepository.GetExpenseGroupsMethod(userName);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Successfully Retrieved", result.Message);
            Assert.NotNull(result.Result);
        }

        // Exception Test Cases

        [Fact]
        public async Task AddExpenseGroup_ShouldReturnException_Message()
        {
            // Arrange
            var expenseGroupDto = new ExpenseGroupDto
            {
                Group_Name = "Jalsa Trip Three",
                Description = "Group for expenses in jalsa trip three",
                Members = new List<string> { "dileepthondupu8@gmail.com", "rameshupparapalli10@gmail.com" },
                Expenses = 29500.20
            };

            var expectedExpenseGroup = new ExpenseGroup
            {
                Group_Name = expenseGroupDto.Group_Name,
                Description = expenseGroupDto.Description,
                Members = expenseGroupDto.Members,
                Expenses = expenseGroupDto.Expenses
            };

            _mockMapper.Setup(m => m.Map<ExpenseGroup>(expenseGroupDto)).Returns(expectedExpenseGroup);

            var ExceptionMessage = "Exception Occurred while adding expenseGroup";

            _mockDbContext.Setup(db => db.ExpenseGroups).Throws(new Exception(ExceptionMessage));


            // Act
            var result = await _expenseRepository.AddExpenseGroupMethod(expenseGroupDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal($"An error occurred: {ExceptionMessage}", result.Message);
        }

        [Fact]
        public async Task AddExpense_shouldReturn_Exception()
        {
            // Arrange
            var addExpenseDto = new ExpenseDto
            {
                GroupId = 12,
                Description = "Jalsa 2024 Travell Controller",
                Amount = 12500.90,
                Paid_by = "dileepthondupu8@gmail.com",
                Split_among = new List<string> { "dileepthondupu8@gmail.com", "rameshupparapalli10@gmail.com" }
            };

            var ExpectedExpense = new Data_Access_Layer.Models.Expense
            {
                GroupId = addExpenseDto.GroupId,
                Description = addExpenseDto.Description,
                Amount = addExpenseDto.Amount,
                Paid_by = addExpenseDto.Paid_by,
                Split_among = addExpenseDto.Split_among
            };

            _mockMapper.Setup(m => m.Map<Data_Access_Layer.Models.Expense>(addExpenseDto)).Returns(ExpectedExpense);

            var ExceptionMessage = "Exception Occurred while adding expense";

            _mockDbContext.Setup(db => db.Expenses).Throws(new Exception(ExceptionMessage));


            // Act
            var result = await _expenseRepository.AddExpenseMethod(addExpenseDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal($"An error occurred: {ExceptionMessage}", result.Message);
        }

        [Fact]
        public async Task UpdateUserPaidExpense_ShouldReturn_Exception()
        {
            // Arrange
            var expense = new Data_Access_Layer.Models.Expense
            {
                ExpenseID = 2,
                GroupId = 12,
                Description = "Jalsa Food Expense Delhi",
                Amount = 5400.90,
                Paid_by = "rameshupparapalli10@gmail.com",
                Split_among = new List<string> { "dileepthondupu8@gmail.com", "rameshupparapalli10@gmail.com" },
                Paid_members = new List<string> { "rameshupparapalli10@gmail.com" },
                Issettled = false
            };

            var ExceptionMessage = "Some Exception Occurred while updating Expense";

            _mockDbContext.Setup(db => db.Expenses).Throws(new Exception(ExceptionMessage));

            // Act
            var result = await _expenseRepository.UpdateUserPaidExpenseMethod(expense);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal($"An error occurred: {ExceptionMessage}", result.Message);
        }

        [Fact]
        public async Task GetExpenseGroups_shouldReturn_Exception()
        {
            //Arrange 
            var UserID = "sampleuser1@gmail.com";
            var users = new List<User> { new User { Username = UserID, Role = "Administrator" },
                                        new User { Username = "sampleuser10@gmail.com", Role = "User"},
                                        new User{ Username = "sampleuser12@gmail.com", Role = "User" } };

            var ExceptionMessage = "Some Exception Occurred while fetching expenseGroups";

            _mockDbContext.Setup(db => db.Users).ReturnsDbSet(users);
            _mockDbContext.Setup(db => db.ExpenseGroups).Throws(new Exception(ExceptionMessage));

            //Act
            var result = await _expenseRepository.GetExpenseGroupsMethod(UserID);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal($"An error occurred: {ExceptionMessage}", result.Message);
        }
    }

}
