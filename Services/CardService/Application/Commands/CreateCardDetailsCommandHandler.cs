using CardService.Application.ViewModel;
using Domain.Common;
using Domain.Common.Repositories;
using MediatR;
using SharedExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace CardService.Application.Commands
{
    /// <summary>
    /// Create card details command handler calls card repository to create card details
    /// </summary>
    public class CreateCardDetailsCommandHandler : IRequestHandler<CreateCardDetailsCommand, CardDetailsViewModel>
    {
        private readonly ICardRepository _cardRepository;

        public CreateCardDetailsCommandHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        /// <summary>
        /// call Card repository to add card details
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CardDetailsViewModel> Handle(CreateCardDetailsCommand request, CancellationToken cancellationToken)
        {
           var cardDetails = await _cardRepository.AddCardDetailsAsync(new Card
            {
                CardCurrency = request.CardCurrency,
                CardNumber = request.CardNumber,
                Cvv = request.Cvv,
                ExpiryMonth = request.ExpiryMonth,
                ExpiryYear = request.ExpiryYear,
                FirstLineOfAddress = request.FirstLineOfAddress,
                CardHolderName = request.CardHolderName,
                PostCode = request.PostCode
            });

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
