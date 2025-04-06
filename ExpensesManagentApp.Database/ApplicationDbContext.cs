using ExpensesManagementApp.Database.DbModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpensesManagementApp.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<DbModels.File> Files { get; set; }
        public DbSet<TransactionGroup> TransactionGroups { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
