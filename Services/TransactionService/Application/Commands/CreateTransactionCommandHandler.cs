using Domain.Common;
using Domain.Common.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace TransactionService.Application.Commands
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, long>
    {
        private readonly ITransactionRepository _transactionRepository;

        public CreateTransactionCommandHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        /// <summary>
        /// Call repository to add new transaction
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<long> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            return _transactionRepository.AddAsync(new Transaction()
            {
                Amount = request.Amount,
                MerchantID = request.MerchantID,
                TransactionStatus = (int)TransactionStatus.submitted,
                CardID = request.CardID,
                IdempotentID = request.IdempotentID
            });
        }
    }
}
