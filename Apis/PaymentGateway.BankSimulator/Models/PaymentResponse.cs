namespace PaymentGateway.BankSimulator.Models
{
    public class PaymentResponse
    {
        public long ResponseId { get; set; }
        public int TransactionStatus { get; set; }
        public string Description { get; set; }
    }
}
