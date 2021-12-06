using Domain.Common.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace TransactionService.Application.Commands
{
    public class UpdateTransactionStatusCommandHandler : IRequestHandler<UpdateTransactionStatusCommand>
    {
        private readonly ITransactionRepository _transactionRepository;

        public UpdateTransactionStatusCommandHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        /// <summary>
        /// call repository update method to update tx status
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(UpdateTransactionStatusCommand request, CancellationToken cancellationToken)
        {
           await _transactionRepository.UpdateStatusAsync(request.TransactionId, request.TransactionStatus);

            return Unit.Value;
        }
    }
}
