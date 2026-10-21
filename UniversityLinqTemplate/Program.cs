using System;
using System.Globalization;
using UniversityLinqTemplate.Data;
using UniversityLinqTemplate.Queries;

namespace UniversityLinqTemplate
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            using (var db = new BankContext())
            {
                Console.WriteLine("Инициализация базы данных банка...");
                DbInitializer.Initialize(db);
                Console.WriteLine("База данных успешно создана и наполнена тестовыми данными!\n");

                // Вызовы демонстрационных LINQ запросов
                LinqDemos.ShowFilteredAccounts(db);
                LinqDemos.ShowSortedTransactions(db);
                LinqDemos.ShowClientShortInfo(db);
                LinqDemos.ShowBankStatistics(db);
                LinqDemos.ShowSummaryByType(db);
                LinqDemos.ShowFullDataWithInclude(db);
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
