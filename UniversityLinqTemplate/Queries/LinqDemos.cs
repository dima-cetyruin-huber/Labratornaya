using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UniversityLinqTemplate.Data;

namespace UniversityLinqTemplate.Queries
{
    public static class LinqDemos
    {
        // 1. Фильтрация (Where) — с несколькими параметрами (&&, ||)
        public static void ShowFilteredAccounts(BankContext db)
        {
            Console.WriteLine("--- 1. Активные дебетовые счета с балансом > 10 000 ---");

            var accounts = db.Accounts
                .Where(a => a.AccountType == "Debit" && a.Balance > 10000m && a.Client!.Status == "Active")
                .ToList();

            foreach (var a in accounts)
            {
                Console.WriteLine($"Счет: {a.AccountNumber}, Баланс: {a.Balance} руб.");
            }
        }

        // 2. Сортировка (OrderBy / OrderByDescending)
        public static void ShowSortedTransactions(BankContext db)
        {
            Console.WriteLine("\n--- 2. Транзакции: сортировка по сумме (убывание), затем по дате ---");

            var transactions = db.Transactions
                .OrderByDescending(t => t.Amount)
                .ThenBy(t => t.Date)
                .ToList();

            foreach (var t in transactions)
            {
                Console.WriteLine($"Сумма: {t.Amount} руб. | Категория: {t.Category} | Дата: {t.Date.ToShortDateString()}");
            }
        }

        // 3. Проекция (Select) — создание анонимного типа
        public static void ShowClientShortInfo(BankContext db)
        {
            Console.WriteLine("\n--- 3. Проекция: краткая информация о клиентах ---");

            var shortInfo = db.Clients
                .Select(c => new
                {
                    c.FullName,
                    Contact = c.PhoneNumber,
                    IsBlocked = c.Status == "Blocked"
                })
                .ToList();

            foreach (var item in shortInfo)
            {
                Console.WriteLine($"ФИО: {item.FullName} | Тел: {item.Contact} | Заблокирован: {item.IsBlocked}");
            }
        }

        // 4. Агрегация (Count, Sum, Average, Min, Max)
        public static void ShowBankStatistics(BankContext db)
        {
            Console.WriteLine("\n--- 4. Общая финансовая агрегация по банку ---");

            int totalAccounts = db.Accounts.Count();
            decimal totalLiquidity = db.Accounts.Sum(a => a.Balance);
            decimal averageBalance = db.Accounts.Average(a => a.Balance);
            decimal maxBalance = db.Accounts.Max(a => a.Balance);

            Console.WriteLine($"Всего открытых счетов: {totalAccounts}");
            Console.WriteLine($"Общий объем средств в банке: {totalLiquidity} руб.");
            Console.WriteLine($"Средний баланс счета: {Math.Round(averageBalance, 2)} руб.");
            Console.WriteLine($"Максимальный баланс на одном счете: {maxBalance} руб.");
        }

        // 5. Группировка (GroupBy) с расчетом агрегатных показателей
        public static void ShowSummaryByType(BankContext db)
        {
            Console.WriteLine("\n--- 5. Группировка счетов по их типам ---");

            var groupedGroups = db.Accounts
                .GroupBy(a => a.AccountType)
                .Select(g => new
                {
                    Type = g.Key,
                    Count = g.Count(),
                    TotalAmount = g.Sum(a => a.Balance)
                })
                .ToList();

            foreach (var group in groupedGroups)
            {
                Console.WriteLine($"Тип счета: {group.Type} | Количество: {group.Count} | Всего денег: {group.TotalAmount} руб.");
            }
        }

        // 6. Жадная загрузка связей (Include, ThenInclude)
        public static void ShowFullDataWithInclude(BankContext db)
        {
            Console.WriteLine("\n--- 6. Жадная загрузка: Клиент -> Счета -> Транзакции ---");

            var clientsWithData = db.Clients
                .Include(c => c.Accounts)
                    .ThenInclude(a => a.Transactions)
                .ToList();

            foreach (var client in clientsWithData)
            {
                Console.WriteLine($"Клиент: {client.FullName} (Статус: {client.Status})");
                foreach (var acc in client.Accounts)
                {
                    Console.WriteLine($"  └ Счет № {acc.AccountNumber} [{acc.AccountType}] | Баланс: {acc.Balance} руб.");
                    foreach (var trans in acc.Transactions)
                    {
                        Console.WriteLine($"      └ Транзакция: {trans.Amount} руб. ({trans.Category}) от {trans.Date.ToShortDateString()}");
                    }
                }
            }
        }
    }
}
