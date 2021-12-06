using MediatR;
using System.Collections.Generic;
using TransactionService.Application.ViewModel;

namespace TransactionService.Application.Queries
{
    public class GetTransactionsByMerchantIDQuery : IRequest<List<TransactionViewModel>>
    {
        public long MerchantID { get; set; }
    }
}
