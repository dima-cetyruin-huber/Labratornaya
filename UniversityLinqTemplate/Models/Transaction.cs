using System;

namespace UniversityLinqTemplate.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; } = "Transfer"; // Transfer, Payment, Cashback

        // Внешний ключ и навигационное свойство на Счет
        public int AccountId { get; set; }
        public Account? Account { get; set; }
    }
}
