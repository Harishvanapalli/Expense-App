using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO_s;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Expense.API.helpers
{
    public class JwtTokenHandlerClass
    {
        private readonly ExpenseDbContext _dbContext;
        public JwtTokenHandlerClass(ExpenseDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        private const int Expires_In = 30;
        public const string SecurityKey = "IamCreatingtheJwtTokenExpense123";
        public virtual AuthenticationResponse? CreateToken(UserDto user)
        {
            
                if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password)) return null;

                var CheckUser = _dbContext.Users.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();
                if (CheckUser == null) return null;

                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

                var claimsIdentity = new ClaimsIdentity(new List<Claim>
                {
                new Claim(ClaimTypes.Name, CheckUser.Username),
                new Claim(ClaimTypes.Role, CheckUser.Role)
                });

                var ExpiresInTime = DateTime.Now.AddMinutes(Expires_In);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));

                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenDescripter = new SecurityTokenDescriptor
                {
                    Subject = claimsIdentity,
                    Expires = ExpiresInTime,
                    SigningCredentials = credentials
                };

                var Securitytoken = jwtSecurityTokenHandler.CreateToken(tokenDescripter);

                var token = jwtSecurityTokenHandler.WriteToken(Securitytoken);

                var response = new AuthenticationResponse
                {
                    Username = CheckUser.Username,
                    Token = token,
                    Expires_In = (int)ExpiresInTime.Subtract(DateTime.Now).TotalMinutes
                };

                return response;

            
        }
    }
}
