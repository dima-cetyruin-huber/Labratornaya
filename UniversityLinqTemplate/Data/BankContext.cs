using Microsoft.EntityFrameworkCore;
using UniversityLinqTemplate.Models;

namespace UniversityLinqTemplate.Data
{
    public class BankContext : DbContext
    {
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Transaction> Transactions => Set<Transaction>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Меняем название файла базы данных на bank.db
            optionsBuilder.UseSqlite("Data Source=bank.db");
        }
    }
}
