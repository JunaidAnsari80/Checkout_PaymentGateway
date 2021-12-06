using CardService.Application.ViewModel;
using MediatR;

namespace CardService.Application.Commands
{
    /// <summary>
    /// Create card details command
    /// </summary>
    public class CreateCardDetailsCommand : IRequest<CardDetailsViewModel>
    {      
        public string CardNumber { get; set; }       
        public string Cvv { get; set; }        
        public string CardHolderName { get; set; }      
        public int ExpiryMonth { get; set; }       
        public int ExpiryYear { get; set; }      
        public string CardCurrency { get; set; }        
        public string FirstLineOfAddress { get; set; }       
        public string PostCode { get; set; }
    }   
}
