using CardService.Application.Queries;
using Domain.Common;
using Domain.Common.Repositories;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace CardService.Tests.Application.Queries
{
    [TestFixture]
    public class GetCardDetailsQueryHandlerTests
    {
        private Mock<ICardRepository> _cardRepository;

        [SetUp]
        public void SetUp()
        {
            _cardRepository = new Mock<ICardRepository>();        
        }

        [Test]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            _cardRepository.Setup(x => x.GetCardDetailsByCardNumberAsync(It.IsAny<string>())).ReturnsAsync(new Card
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
            });

            var sut = new GetCardDetailsQueryHandler(_cardRepository.Object);
            GetCardDetailsQuery request = new GetCardDetailsQuery { CardNumber = "5295650000000000"};
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await sut.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            _cardRepository.Verify(x => x.GetCardDetailsByCardNumberAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task Handle_StateUnderTest_UnexpectedBehavior()
        {
            // Arrange
            _cardRepository.Setup(x => x.GetCardDetailsByCardNumberAsync(It.IsAny<string>())).ReturnsAsync(default(Card));

            var sut = new GetCardDetailsQueryHandler(_cardRepository.Object);
            GetCardDetailsQuery request = new GetCardDetailsQuery { CardNumber = "5295650000000000" };
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await sut.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.IsNull(result);
            _cardRepository.Verify(x => x.GetCardDetailsByCardNumberAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
