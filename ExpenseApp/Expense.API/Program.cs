using AutoMapper;
using Business_Layer.Repository.AdminRepository;
using Business_Layer.Repository.ExpenseRepository;
using Data_Access_Layer.Data;
using Expense.API.helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

IMapper mapper = MappingCongigure.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<ExpenseDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ExpenseConnection")));

builder.Services.AddTransient<JwtTokenHandlerClass>();

//Experiment
builder.Services.AddScoped<IAdminInterface, AdminInterface>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("IamCreatingtheJwtTokenExpense123"))

    };
});

builder.Services.AddCors(options => options.AddPolicy(name: "ExpensePolicy", policy =>
{
    policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("ExpensePolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
