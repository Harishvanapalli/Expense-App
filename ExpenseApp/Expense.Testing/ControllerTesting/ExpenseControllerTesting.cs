using Business_Layer.Repository.ExpenseRepository;
using Data_Access_Layer.Models.DTO_s;
using Data_Access_Layer.Models;
using Expense.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Layer.Repository.AdminRepository;

namespace Expense.Testing.ControllerTesting
{
    public class ExpenseControllerTesting
    {
        private readonly Mock<IExpenseRepository> _expenseRepository;

        private readonly ExpenseController _expenseController;

        public ExpenseControllerTesting()
        {
            this._expenseRepository = new Mock<IExpenseRepository>();

            this._expenseController = new ExpenseController(_expenseRepository.Object);
        }

        [Fact]
        public async Task AddExpenseGroup_shouldAddExpenseGroup()
        {
            // Arrange
            var expenseGroupDto = new ExpenseGroupDto
            {
                Group_Name = "Jalsa Trip Three",
                Description = "Group for expenses in jalsa trip three",
                Members = new List<string> { "dileepthondupu8@gmail.com", "rameshupparapalli10@gmail.com" },
                Expenses = 29500.20
            };

            var expectedResponse = new ResponseClass
            {
                Message = "Expense Group Created Successfully",
                IsSuccess = true
            };

            _expenseRepository.Setup(x => x.AddExpenseGroupMethod(It.IsAny<ExpenseGroupDto>()))
                              .ReturnsAsync(expectedResponse);

            // Act
            var result = await _expenseController.AddExpenseGroup(expenseGroupDto);

            // Assert
            var okObjectResult = Assert.IsType<ActionResult<ResponseClass>>(result);
            var responseData = Assert.IsType<ResponseClass>(okObjectResult.Value);

            Assert.Equal(expectedResponse.Message, responseData.Message);
            Assert.Equal(expectedResponse.IsSuccess, responseData.IsSuccess);

        }

        [Fact]
        public async Task AddExpense_shoulAddExpense()
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

            var expectedResponse = new ResponseClass
            {
                Message = "Expense Added Successfully",
                IsSuccess = true
            };

            _expenseRepository.Setup(x => x.AddExpenseMethod(It.IsAny<ExpenseDto>()))
                              .ReturnsAsync(expectedResponse);


            // Act
            var result = await _expenseController.AddExpense(addExpenseDto);

            // Assert
            var okObjectResult = Assert.IsType<ActionResult<ResponseClass>>(result);
            var responseData = Assert.IsType<ResponseClass>(okObjectResult.Value);

            Assert.Equal(expectedResponse.Message, responseData.Message);
            Assert.Equal(expectedResponse.IsSuccess, responseData.IsSuccess);
        }

        [Fact]
        public async Task UpdateUserPaidExpense_ShouldUpdateExpense()
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

            var expectedResponse = new ResponseClass
            {
                Message = "Successfully Paid the Expense",
                IsSuccess = true
            };

            _expenseRepository.Setup(x => x.UpdateUserPaidExpenseMethod(It.IsAny<Data_Access_Layer.Models.Expense>()))
                              .ReturnsAsync(expectedResponse);

            // Act
            var result = await _expenseController.UpdateUserPaidExpense(expense);

            // Assert
            var okObjectResult = Assert.IsType<ActionResult<ResponseClass>>(result);
            var responseData = Assert.IsType<ResponseClass>(okObjectResult.Value);

            Assert.Equal(expectedResponse.Message, responseData.Message);
            Assert.Equal(expectedResponse.IsSuccess, responseData.IsSuccess);
        }

        [Fact]
        public async Task GetExpenseGroups_shouldReturnExpenseGroups()
        {
            //Arrange 
            var UserID = "sampleuser1@gmail.com";

            var ExpenseGroups = new List<ExpensesGroupResponseDto>
            {
                new ExpensesGroupResponseDto { ExpenseGroupID = 1, Group_Name="Sample Group", Description = "Sample Group for Testing",
                    Created_Date = DateTime.Now, Members = new List<string>{ "sampleuser1@gmail.com", "sampleuser2@gmail.com" }, Expenses= 12500.90,
                    GroupExpenses = new List<Data_Access_Layer.Models.Expense>
                    {
                        new Data_Access_Layer.Models.Expense{ExpenseID = 1, GroupId = 1, Description = "Sample Expense for Testing",
                            Amount= 2500.90, Paid_by = "sampleuser1@gmail.com", Created_date = DateTime.Now, 
                            Split_among = new List<string>{"sampleuser1@gmail.com", "sampleuser2@gmail.com" },
                            Paid_members = new List<string>{"sampleuser1@gmail.com" }, Issettled = false
                        }
                    }
                }
            };

            var expectedResponse = new CommonResponse
            {
                Result = ExpenseGroups,
                Message = "Successfully Retrieved",
                IsSuccess = true
            };

            _expenseRepository.Setup(x => x.GetExpenseGroupsMethod(UserID)).ReturnsAsync(expectedResponse);

            //Act
            var result = await _expenseController.GetExpenseGroups(UserID);

            //Assert
            var okObjectResult = Assert.IsType<ActionResult<CommonResponse>>(result);
            var responseData = Assert.IsType<CommonResponse>(okObjectResult.Value);

            Assert.Equal(expectedResponse.Result, responseData.Result);
            Assert.Equal(expectedResponse.Message, responseData.Message);
            Assert.Equal(expectedResponse.IsSuccess, responseData.IsSuccess);

            var expenseGoupsList = expectedResponse.Result as List<ExpensesGroupResponseDto>;
            Assert.Equal(1, expenseGoupsList.Count);
            Assert.Equal("Sample Group", expenseGoupsList[0].Group_Name);
            Assert.Equal("Sample Group for Testing", expenseGoupsList[0].Description);
            Assert.Equal(12500.90, expenseGoupsList[0].Expenses);
            Assert.Equal(2, expenseGoupsList[0].Members.Count);

            var members = new List<string> { "sampleuser1@gmail.com", "sampleuser2@gmail.com" };
            Assert.Equal(members, expenseGoupsList[0].Members);
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

            var ExceptionMessage = "Exception Occurred";

            _expenseRepository.Setup(x => x.AddExpenseGroupMethod(expenseGroupDto)).ThrowsAsync(new Exception(ExceptionMessage));


            // Act
            var result = await _expenseController.AddExpenseGroup(expenseGroupDto);

            // Assert
            var okObjectResult = Assert.IsType<ActionResult<ResponseClass>>(result);
            var responseData = Assert.IsType<ResponseClass>(okObjectResult.Value);

            Assert.False(responseData.IsSuccess);
            Assert.Equal($"An error occurred {ExceptionMessage}", responseData.Message);
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

            var ExceptionMessage = "Exception Occurred";

            _expenseRepository.Setup(x => x.AddExpenseMethod(addExpenseDto)).ThrowsAsync(new Exception(ExceptionMessage));


            // Act
            var result = await _expenseController.AddExpense(addExpenseDto);

            // Assert
            var okObjectResult = Assert.IsType<ActionResult<ResponseClass>>(result);
            var responseData = Assert.IsType<ResponseClass>(okObjectResult.Value);

            Assert.False(responseData.IsSuccess);
            Assert.Equal($"An error occurred {ExceptionMessage}", responseData.Message);
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

            var ExceptionMessage = "Some Exception Occurred";

            _expenseRepository.Setup(x => x.UpdateUserPaidExpenseMethod(expense)).ThrowsAsync(new Exception(ExceptionMessage));

            // Act
            var result = await _expenseController.UpdateUserPaidExpense(expense);

            // Assert
            var okObjectResult = Assert.IsType<ActionResult<ResponseClass>>(result);
            var responseData = Assert.IsType<ResponseClass>(okObjectResult.Value);

            Assert.False(responseData.IsSuccess);
            Assert.Equal($"An error occurred {ExceptionMessage}", responseData.Message);
        }

        [Fact]
        public async Task GetExpenseGroups_shouldReturn_Exception()
        {
            //Arrange 
            var UserID = "sampleuser1@gmail.com";

            var ExpenseGroups = new List<ExpensesGroupResponseDto>
            {
                new ExpensesGroupResponseDto { ExpenseGroupID = 1, Group_Name="Sample Group", Description = "Sample Group for Testing",
                    Created_Date = DateTime.Now, Members = new List<string>{ "sampleuser1@gmail.com", "sampleuser2@gmail.com" }, Expenses= 12500.90,
                    GroupExpenses = new List<Data_Access_Layer.Models.Expense>
                    {
                        new Data_Access_Layer.Models.Expense{ExpenseID = 1, GroupId = 1, Description = "Sample Expense for Testing",
                            Amount= 2500.90, Paid_by = "sampleuser1@gmail.com", Created_date = DateTime.Now,
                            Split_among = new List<string>{"sampleuser1@gmail.com", "sampleuser2@gmail.com" },
                            Paid_members = new List<string>{"sampleuser1@gmail.com" }, Issettled = false
                        }
                    }
                }
            };

            var ExceptionMessage = "Some Exception Occurred";

            _expenseRepository.Setup(x => x.GetExpenseGroupsMethod(UserID)).ThrowsAsync(new Exception(ExceptionMessage));

            //Act
            var result = await _expenseController.GetExpenseGroups(UserID);

            //Assert
            var okObjectResult = Assert.IsType<ActionResult<CommonResponse>>(result);
            var responseData = Assert.IsType<CommonResponse>(okObjectResult.Value);

            Assert.False(responseData.IsSuccess);
            Assert.Equal($"An error occurred {ExceptionMessage}", responseData.Message);
        }
    }
}
