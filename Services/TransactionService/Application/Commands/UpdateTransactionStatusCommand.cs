using MediatR;

namespace TransactionService.Application.Commands
{
    /// <summary>
    /// Update Transaction command request command
    /// </summary>
    public class UpdateTransactionStatusCommand : IRequest
    {
        public long TransactionId { get; set; }
        public int TransactionStatus { get; set; }
    }
}
