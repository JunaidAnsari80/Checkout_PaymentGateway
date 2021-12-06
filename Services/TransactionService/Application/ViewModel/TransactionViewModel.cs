namespace TransactionService.Application.ViewModel
{
    public class TransactionViewModel
    {
        public long TransactionID { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public string MaskedCardNumber { get; set; }
        public string CardHolderName { get; set; }
        public long MerchantID { get; set; }
        public int TransactionStatus { get; set; }
    }
}
