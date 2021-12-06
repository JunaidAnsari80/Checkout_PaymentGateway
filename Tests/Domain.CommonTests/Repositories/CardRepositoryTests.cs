using Domain.Common;
using Domain.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Domain.CommonTests.Repositories
{
    [TestFixture]
    public class CardRepositoryTests
    {
        private PaymentGatewayDbContext _PaymentGatewayDbContext;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<PaymentGatewayDbContext>()
                       .UseInMemoryDatabase(databaseName: "Test")
                       .Options;

            _PaymentGatewayDbContext = new PaymentGatewayDbContext(options);
        }

        private CardRepository CreateCardRepository()
        {
            return new CardRepository(_PaymentGatewayDbContext);
        }

        [Test]
        public async Task AddCardDetailsAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var cardRepository = CreateCardRepository();

            Card card = new Card
            {
                CardID = 1,
                CardCurrency = "GBP",
                CardHolderName = "JamesBond",
                CardNumber = "5295650000000000",
                Cvv = "333",
                ExpiryMonth = 10,
                ExpiryYear = 2020,
                FirstLineOfAddress = "adr",
                PostCode = "PC"
            };

            // Act
            var result = await cardRepository.AddCardDetailsAsync(card);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.CardID > 0);
        }

        [Test]
        public async Task GetCardDetailsByCardNumberAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            var cardRepository = CreateCardRepository();
            string cardNumber = "5295650000000001";
            var card = new Card
            {              
                CardCurrency = "GBP",
                CardHolderName = "JamesBond",
                CardNumber = "5295650000000001",
                Cvv = "333",
                ExpiryMonth = 10,
                ExpiryYear = 2020,
                FirstLineOfAddress = "adr",
                PostCode = "PC"
            };
           
            await cardRepository.AddCardDetailsAsync(card);

            // Act
            var result = await cardRepository.GetCardDetailsByCardNumberAsync(
                cardNumber);

            //    // Assert
            Assert.IsNotNull(result);

        }
    }
}
