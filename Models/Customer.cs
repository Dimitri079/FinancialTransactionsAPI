using FinancialTransactionsAPI.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public string FullName { get; set; }

    public ICollection<PhoneNumber> PhoneNumbers { get; set; }
    public ICollection<EmailAddress> EmailAddresses { get; set; }
    public ICollection<Address> Addresses { get; set; }
}
