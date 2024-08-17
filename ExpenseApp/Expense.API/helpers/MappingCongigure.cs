using AutoMapper;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO_s;

namespace Expense.API.helpers
{
    public class MappingCongigure
    {
        public static MapperConfiguration RegisterMaps()
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.CreateMap<ExpenseGroupDto, ExpenseGroup>();
                config.CreateMap<ExpenseGroup, ExpenseGroupDto>();
                config.CreateMap<ExpenseGroup, ExpensesGroupResponseDto>();
                config.CreateMap<ExpenseDto, Data_Access_Layer.Models.Expense>();
                config.CreateMap<User, UserDto>();
            });
            return configuration;
        }
    }
}
