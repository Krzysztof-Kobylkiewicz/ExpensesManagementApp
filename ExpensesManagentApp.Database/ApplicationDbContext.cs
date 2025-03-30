using ExpensesManagementApp.Database.DbModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpensesManagementApp.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpensesManagementApp.Database.DbModels.File> Files { get; set; }
    }
}
