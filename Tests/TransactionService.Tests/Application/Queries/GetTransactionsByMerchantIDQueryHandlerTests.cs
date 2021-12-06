using Domain.Common;
using Domain.Common.Repositories;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TransactionService.Application.Queries;

namespace TransactionService.Tests.Application.Queries
{
    [TestFixture]
    public class GetTransactionsByMerchantIDQueryHandlerTests
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
            _transactionRepository.Setup(x => x.GetTransactionsByMerchantIdAsync(It.IsAny<long>())).ReturnsAsync(new List<Transaction>() { new Transaction() { CardDetails = new Card {CardNumber = "5295650000000001" } } });

            var sut = new GetTransactionsByMerchantIDQueryHandler(_transactionRepository.Object);

            GetTransactionsByMerchantIDQuery request = new GetTransactionsByMerchantIDQuery
            {
                MerchantID = 10
            };

            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await sut.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result.Count > 0);
        }

        [Test]
        public async Task Handle_StateUnderTest_UnexpectedBehavior()
        {
            // Arrange
            _transactionRepository.Setup(x => x.GetTransactionsByMerchantIdAsync(It.IsAny<long>())).ReturnsAsync(new List<Transaction>() {});

            var sut = new GetTransactionsByMerchantIDQueryHandler(_transactionRepository.Object);

            GetTransactionsByMerchantIDQuery request = new GetTransactionsByMerchantIDQuery
            {
                MerchantID = 10
            };

            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await sut.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result.Count == 0);
        }
    }
}
