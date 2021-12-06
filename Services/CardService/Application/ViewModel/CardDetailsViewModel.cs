namespace CardService.Application.ViewModel
{
    public class CardDetailsViewModel
    {
        public long CardDetailsID { get; set; }

        public string CardNumber { get; set; }

        public string Cvv { get; set; }

        public string HolderName { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public string CardCurrency { get; set; }

        public string FirstLineOfAddress { get; set; }

        public string PostCode { get; set; }
    }
}
