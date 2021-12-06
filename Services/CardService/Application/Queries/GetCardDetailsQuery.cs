using CardService.Application.ViewModel;
using MediatR;

namespace CardService.Application.Queries
{
    public class GetCardDetailsQuery:IRequest<CardDetailsViewModel>
    {
        public string CardNumber { get; set; }
    }
}
