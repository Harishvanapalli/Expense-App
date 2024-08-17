using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Data
{
    public class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext() { }
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options)
        {
                
        }

        public virtual DbSet<Expense> Expenses { get; set; }

        public virtual DbSet<ExpenseGroup> ExpenseGroups { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Expense>().HasOne<ExpenseGroup>().WithMany().HasForeignKey(k => k.GroupId);

            modelBuilder.Entity<User>().HasData(
             new User
             {
                 UserID = 1,
                 Username = "harishvanapalli9@gmail.com",
                 Password = "Harish@123",
                 Role = "Administrator"
             },
             new User
             {
                 UserID = 2,
                 Username = "ravivanapalli9@gmail.com",
                 Password = "Ravi@123",
                 Role = "Administrator"
             },
             new User
             {
                 UserID = 3,
                 Username = "dileepthondupu8@gmail.com",
                 Password = "Dileep@123",
                 Role = "User"
             },
             new User
             {
                 UserID = 4,
                 Username = "mohanuchula10@gmail.com",
                 Password = "Mohan@123",
                 Role = "User"
             },
             new User
             {
                 UserID = 5,
                 Username = "rameshupparapalli108@gmail.com",
                 Password = "Ramesh@123",
                 Role = "User"
             },
             new User
             {
                 UserID = 6,
                 Username = "naveenbuddha9@gmail.com",
                 Password = "Naveen@123",
                 Role = "User"
             }
           );

        }
    }
}
