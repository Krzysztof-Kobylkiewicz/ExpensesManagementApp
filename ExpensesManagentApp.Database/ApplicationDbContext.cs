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
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DbModels.File>()
                .HasMany(f => f.Transactions)
                .WithOne(t => t.File)
                .HasForeignKey(f => f.FileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
