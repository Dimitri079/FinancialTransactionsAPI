using System.Text.Json.Serialization;

namespace FinancialTransactionsAPI.Models
{
    public class PhoneNumber
    {
        public int PhoneNumberId { get; set; }
        public string Number { get; set; }
        public int CustomerId { get; set; }
    }
}
