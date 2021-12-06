using Domain.Common;
using Domain.Common.Repositories;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using TransactionService.Application.Commands;

namespace TransactionService.Tests.Application.Commands
{
    [TestFixture]
    public class CreateTransactionCommandHandlerTests
    {
        private Mock<ITransactionRepository> _transactionRepository;

        [SetUp]
        public void SetUp()
        {
            _transactionRepository = new Mock<ITransactionRepository>();           
        }

        [Test]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            _transactionRepository.Setup(x => x.AddAsync(It.IsAny<Transaction>())).ReturnsAsync(1);
            var createTransactionCommandHandler = new CreateTransactionCommandHandler(_transactionRepository.Object);
            CreateTransactionCommand request = new CreateTransactionCommand
            {
                Amount = 10,
                CardCurrency = "GBP",
                CardHolderName = "JamesBond",
                CardID = 1,
                CardNumber = "5295650000000000",
                Cvv = "222",
                ExpiryMonth = 10,
                ExpiryYear = 2020,
                FirstLineOfAddress = "address",
                IdempotentID = "1A",
                MerchantID = 22,
                PostCode = "DA15"
            };

            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await createTransactionCommandHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            _transactionRepository.Verify(x => x.AddAsync(It.IsAny<Transaction>()), Times.Once);
        }       
    }
}
