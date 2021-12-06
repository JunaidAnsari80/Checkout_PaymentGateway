namespace PaymentService.Application.ViewModel
{
    /// <summary>
    /// Payment response model
    /// </summary>
    public class PaymentResponseViewModel
    {
        public long ResponseId { get; set; }
        public int TransactionStatus { get; set; }
        public string Description { get; set; }        
    }
}
