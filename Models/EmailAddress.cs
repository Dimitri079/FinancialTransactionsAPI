using System.Text.Json.Serialization;

namespace FinancialTransactionsAPI.Models
{
    public class EmailAddress
    {
        public int EmailAddressId { get; set; }
        public string Email { get; set; }
        public int CustomerId { get; set; }

    }
}
