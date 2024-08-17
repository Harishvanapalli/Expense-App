using Data_Access_Layer.Models.DTO_s;
using Data_Access_Layer.Models;
using Expense.API.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Testing.HelperClassesTesting
{
    public class MappingConfigureTests
    {

        private readonly ExpenseGroupDto expenseGroupDto;
        private readonly ExpenseGroup expenseGroup;
        private readonly Data_Access_Layer.Models.Expense expense;
        private readonly ExpenseDto expenseDto;
        private readonly User user;
        private readonly UserDto userDto;
        private readonly ExpensesGroupResponseDto expensesGroupResonseDto;

        public MappingConfigureTests()
        {
            expenseGroup = new ExpenseGroup
            {
                ExpenseGroupID = 1,
                Group_Name = "Jalsa Trip Three",
                Description = "Group for expenses in jalsa trip three",
                Created_Date = DateTime.Now,
                Members = new List<string> { "dileepthondupu8@gmail.com", "rameshupparapalli10@gmail.com" },
                Expenses = 29500.20
            };
            expenseGroupDto = new ExpenseGroupDto
            {
                Group_Name = "Jalsa Trip Three",
                Description = "Group for expenses in jalsa trip three",
                Members = new List<string> { "dileepthondupu8@gmail.com", "rameshupparapalli10@gmail.com" },
                Expenses = 29500.20
            };
            expense = new Data_Access_Layer.Models.Expense
            {
                ExpenseID = 1,
                GroupId = 1,
                Description = "Jalsa Cab Expense Delhi",
                Amount = 2400.90,
                Paid_by = "rameshupparapalli10@gmail.com",
                Split_among = new List<string> { "dileepthondupu8@gmail.com", "rameshupparapalli10@gmail.com" },
                Paid_members = new List<string> { "rameshupparapalli10@gmail.com" },
                Issettled = false
            };
            expenseDto = new ExpenseDto
            {
                GroupId = 1,
                Description = "Jalsa Cab Expense Delhi",
                Amount = 2400.90,
                Paid_by = "rameshupparapalli10@gmail.com",
                Split_among = new List<string> { "dileepthondupu8@gmail.com", "rameshupparapalli10@gmail.com" }
            };
            user = new User { Username = "sampleuser1@gmail.com", Password = "Sample@123", Role = "User" };
            userDto = new UserDto { Username = "sampleuser1@gmail.com", Password = "Sample@123" };

            expensesGroupResonseDto = new ExpensesGroupResponseDto
            {
                ExpenseGroupID = 1,
                Group_Name = "Jalsa Trip Three",
                Description = "Group for expenses in jalsa trip three",
                Created_Date = DateTime.Now,
                Members = new List<string> { "dileepthondupu8@gmail.com", "rameshupparapalli10@gmail.com" },
                Expenses = 29500.20,
                GroupExpenses = new List<Data_Access_Layer.Models.Expense> { expense }
            };
        }

        [Fact]
        public void RegisterMaps_shouldValidateMaps()
        {
            //Arrange
            var mapperConfiguration = MappingCongigure.RegisterMaps();
            var mapper = mapperConfiguration.CreateMapper();

            var expenseGroupDestination = mapper.Map<ExpenseGroup>(expenseGroupDto);
            var expenseGroupDtoDestination = mapper.Map<ExpenseGroupDto>(expenseGroup);

            var expenseDestination = mapper.Map<Data_Access_Layer.Models.Expense>(expenseDto);

            var userDtoDestination = mapper.Map<UserDto>(user);

            var expenseGroupResponseDtoDestination = mapper.Map<ExpensesGroupResponseDto>(expenseGroup);

            //Assert
            Assert.NotNull(mapper);
            Assert.NotNull(expenseGroupDestination);
            Assert.NotNull(expenseDestination);
            Assert.NotNull(expenseGroupDtoDestination);
            Assert.NotNull(userDtoDestination);
            Assert.NotNull(expenseGroupResponseDtoDestination);
        }
    }
}
