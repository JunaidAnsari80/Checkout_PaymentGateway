using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Common.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly PaymentGatewayDbContext _dbContext;
        private DbSet<Card> _cards;

        public CardRepository(PaymentGatewayDbContext dbContext)
        {
            _dbContext = dbContext;
            _cards = _dbContext.Set<Card>();
        }

        /// <summary>
        /// Add card details
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public async Task<Card> AddCardDetailsAsync(Card card)
        {
            await _cards.AddAsync(card);
            await _dbContext.SaveChangesAsync();
            return card;
        }
        /// <summary>
        /// GetCard details
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public Task<Card> GetCardDetailsByCardNumberAsync(string cardNumber)
        {
            return _cards.Where(x => x.CardNumber == cardNumber).FirstOrDefaultAsync();
        }
    }
}
