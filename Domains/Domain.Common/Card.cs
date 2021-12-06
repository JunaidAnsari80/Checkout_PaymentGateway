using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
    public class Card
    {
        [Key]
        public long CardID { get; set; }

        public string CardNumber { get; set; }

        public string Cvv { get; set; }

        public string CardHolderName { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public string CardCurrency { get; set; }

        public string FirstLineOfAddress { get; set; }

        public string PostCode { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
