using System.Collections.Generic;

namespace UniversityLinqTemplate.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string AccountType { get; set; } = "Debit"; // Debit, Credit, Deposit

        // Внешний ключ и навигационное свойство на Клиента
        public int ClientId { get; set; }
        public Client? Client { get; set; }

        // Навигационное свойство: история транзакций по счету
        public List<Transaction> Transactions { get; set; } = new();
    }
}
