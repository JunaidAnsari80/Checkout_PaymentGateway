using Domain.Common.Repositories;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using TransactionService.Application.Commands;

namespace TransactionService.Tests.Application.Commands
{
    [TestFixture]
    public class UpdateTransactionStatusCommandHandlerTests
    {
        private Mock<ITransactionRepository> _transactionRepository;

        [SetUp]
        public void SetUp()
        {
            _transactionRepository = new Mock<ITransactionRepository>();

            _transactionRepository.Setup(x => x.UpdateStatusAsync(It.IsAny<long>(), It.IsAny<int>()));
        }

        [Test]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sut = new UpdateTransactionStatusCommandHandler(_transactionRepository.Object);

            UpdateTransactionStatusCommand request = new UpdateTransactionStatusCommand
            {
                TransactionId = 1,
                TransactionStatus = 2
            };
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await sut.Handle(
                request,
                cancellationToken);

            // Assert
            _transactionRepository.Verify(x => x.UpdateStatusAsync(It.IsAny<long>(), It.IsAny<int>()),Times.Once);
        }
    }
}
