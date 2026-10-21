using System.Collections.Generic;

namespace UniversityLinqTemplate.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Status { get; set; } = "Active"; // Active, Blocked

        // Навигационное свойство: у одного клиента может быть несколько счетов
        public List<Account> Accounts { get; set; } = new();
    }
}
