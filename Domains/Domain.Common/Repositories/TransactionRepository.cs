using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Common.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly PaymentGatewayDbContext _dbContext;
        private DbSet<Transaction> _transations;

        public TransactionRepository(PaymentGatewayDbContext dbContext)
        {
            _dbContext = dbContext;
            _transations = _dbContext.Set<Transaction>();
        }

        /// <summary>
        /// return all merchant transactions
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public Task<List<Transaction>> GetTransactionsByMerchantIdAsync(long merchantId)
        {
            return _transations.Include(x => x.CardDetails).Where(x => x.MerchantID == merchantId).ToListAsync();
        }

        /// <summary>
        /// Add new transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<long> AddAsync(Transaction transaction)
        {
            _transations.Add(transaction);
            await _dbContext.SaveChangesAsync();
            return transaction.TransactionID;

        }

        /// <summary>
        /// Update tx status
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="transactionStatus"></param>
        /// <returns></returns>
        public async Task UpdateStatusAsync(long transactionId, int transactionStatus)
        {
            var transaction = await _transations.FirstOrDefaultAsync(x => x.TransactionID == transactionId);
            transaction.TransactionStatus = transactionStatus;
            await _dbContext.SaveChangesAsync();           
        }
    }

}
