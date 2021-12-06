using System.Threading.Tasks;

namespace Domain.Common.Repositories
{
    public interface ICardRepository
    {
        Task<Card> AddCardDetailsAsync(Card card);
        Task<Card> GetCardDetailsByCardNumberAsync(string cardNumber);
    }
}
