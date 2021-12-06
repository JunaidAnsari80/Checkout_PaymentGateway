using Domain.Common.Repositories;
using MediatR;
using SharedExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TransactionService.Application.ViewModel;

namespace TransactionService.Application.Queries
{
    public class GetTransactionsByMerchantIDQueryHandler : IRequestHandler<GetTransactionsByMerchantIDQuery, List<TransactionViewModel>>
    {
        private readonly ITransactionRepository _transactionRepository;

        public GetTransactionsByMerchantIDQueryHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        /// <summary>
        /// Return collection of transacdtion with masked card number
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<TransactionViewModel>> Handle(GetTransactionsByMerchantIDQuery request, CancellationToken cancellationToken)
        {
            var response = await _transactionRepository.GetTransactionsByMerchantIdAsync(request.MerchantID).ConfigureAwait(false);
            return response.Select(x => new TransactionViewModel
            {
                Amount = x.Amount,
                TransactionStatus = x.TransactionStatus,
                TransactionID = x.TransactionID,
                MerchantID = x.MerchantID,
                Currency = x.CardDetails.CardCurrency,
                MaskedCardNumber = x.CardDetails.CardNumber.ToMask(),
                CardHolderName = x.CardDetails.CardHolderName
                
            }).ToList();
        }
    }
}
