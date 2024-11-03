using FinancialTransactionsAPI.Enums;
using System.Text.Json.Serialization;

namespace FinancialTransactionsAPI.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionStatus Status { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmailAddress { get; set; }

        // Foreign key reference to Customer
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
