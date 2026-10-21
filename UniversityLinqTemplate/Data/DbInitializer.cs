using System;
using System.Collections.Generic;
using UniversityLinqTemplate.Models;

namespace UniversityLinqTemplate.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BankContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // 1. Создаем клиентов
            var client1 = new Client { FullName = "Иванов Иван Иванович", PhoneNumber = "+79991112233", Status = "Active" };
            var client2 = new Client { FullName = "Петрова Анна Сергеевна", PhoneNumber = "+79994445566", Status = "Active" };
            var client3 = new Client { FullName = "Сидоров Петр Петрович", PhoneNumber = "+79997778899", Status = "Blocked" };

            // 2. Создаем счета для клиентов
            var acc1 = new Account { AccountNumber = "408020001", Balance = 150000m, AccountType = "Debit", Client = client1 };
            var acc2 = new Account { AccountNumber = "408020002", Balance = 5000m, AccountType = "Credit", Client = client1 };
            var acc3 = new Account { AccountNumber = "408020003", Balance = 870000m, AccountType = "Deposit", Client = client2 };
            var acc4 = new Account { AccountNumber = "408020004", Balance = 0m, AccountType = "Debit", Client = client3 };

            // 3. Создаем транзакции для счетов
            var transactions = new List<Transaction>
            {
                new Transaction { Amount = 5000m, Date = DateTime.Now.AddDays(-2), Category = "Payment", Account = acc1 },
                new Transaction { Amount = 1200m, Date = DateTime.Now.AddDays(-1), Category = "Payment", Account = acc1 },
                new Transaction { Amount = 300m, Date = DateTime.Now, Category = "Cashback", Account = acc1 },
                new Transaction { Amount = 50000m, Date = DateTime.Now.AddDays(-5), Category = "Transfer", Account = acc3 },
                new Transaction { Amount = 15000m, Date = DateTime.Now.AddDays(-3), Category = "Transfer", Account = acc2 }
            };

            // Добавляем всё в контекст и сохраняем
            context.Clients.AddRange(client1, client2, client3);
            context.Accounts.AddRange(acc1, acc2, acc3, acc4);
            context.Transactions.AddRange(transactions);

            context.SaveChanges();
        }
    }
}
