using MediatR;

namespace TransactionService.Application.Commands
{
    /// <summary>
    /// Create transaction input model
    /// </summary>
    public class CreateTransactionCommand : IRequest<long>
    {
        public decimal Amount { get; set; }
        public long MerchantID { get; set; }
        public string IdempotentID { get; set; }
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public string CardHolderName { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CardCurrency { get; set; }
        public string FirstLineOfAddress { get; set; }
        public string PostCode { get; set; }
        public long CardID { get; set; }
    }
}
