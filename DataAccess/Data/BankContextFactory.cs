using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Data
{
    public class BankContextFactory : IDesignTimeDbContextFactory<BankContext>
    {
        public BankContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BankContext>();

            optionsBuilder.UseSqlServer(
                "Server=.\\SQLEXPRESS02;Database=BankSystemDb;Trusted_Connection=True;TrustServerCertificate=True");

            return new BankContext(optionsBuilder.Options);
        }
    }
}