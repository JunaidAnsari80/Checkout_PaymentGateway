using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Common.Repositories
{
    public interface ITransactionRepository
    {
        Task<long> AddAsync(Transaction transaction);
        Task UpdateStatusAsync(long transactionId, int transactionStatus);
        Task<List<Transaction>> GetTransactionsByMerchantIdAsync(long merchantId);
    }
}
