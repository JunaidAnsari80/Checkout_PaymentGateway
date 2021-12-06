using CardService.Application.ViewModel;
using Domain.Common.Repositories;
using MediatR;
using SharedExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace CardService.Application.Queries
{
    public class GetCardDetailsQueryHandler : IRequestHandler<GetCardDetailsQuery, CardDetailsViewModel>
    {
        private readonly ICardRepository _cardRepository;

        public GetCardDetailsQueryHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        /// <summary>
        /// Handles the getcarddetailsquery and return card details VM
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CardDetailsViewModel> Handle(GetCardDetailsQuery request, CancellationToken cancellationToken)
        {
            var cardDetails = await _cardRepository.GetCardDetailsByCardNumberAsync(request.CardNumber);
            if (cardDetails == null)
                return null;

            return new CardDetailsViewModel
            {
                CardCurrency = cardDetails.CardCurrency,
                CardDetailsID = cardDetails.CardID,
                CardNumber = cardDetails.CardNumber.ToMask(),
                Cvv = cardDetails.Cvv,
                ExpiryMonth = cardDetails.ExpiryMonth,
                ExpiryYear = cardDetails.ExpiryYear,
                FirstLineOfAddress = cardDetails.FirstLineOfAddress,
                HolderName = cardDetails.CardHolderName,
                PostCode = cardDetails.PostCode
            };
        }
    }
}
