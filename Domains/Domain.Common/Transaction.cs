using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
    public class Transaction
    {
        [Key]
        public long TransactionID { get; set; }
        public decimal Amount { get; set; }
        public long CardID { get; set; }
        public Card CardDetails { get; set; }
        public long MerchantID { get; set; }
        public int TransactionStatus { get; set; }
        public string IdempotentID { get; set; }
    }
}
