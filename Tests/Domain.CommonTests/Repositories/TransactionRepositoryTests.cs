using Domain.Common;
using Domain.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Domain.CommonTests.Repositories
{
    [TestFixture]
    public class TransactionRepositoryTests
    {
        private PaymentGatewayDbContext _PaymentGatewayDbContext;
        private Transaction transaction;

        [SetUp]
        public void SetUp()
        {
            transaction = new Transaction
            {
                Amount = 10,
                IdempotentID = "1A",
                MerchantID = 22,
                CardDetails = new Card
                {
                    CardCurrency = "GBP",
                    CardHolderName = "JamesBond",
                    CardNumber = "5295650000000000",
                    Cvv = "333",
                    ExpiryMonth = 10,
                    ExpiryYear = 2020,
                    FirstLineOfAddress = "adr",
                    PostCode = "PC"
                }
            };

            var options = new DbContextOptionsBuilder<PaymentGatewayDbContext>()
                       .UseInMemoryDatabase(databaseName: "Test")
                       .Options;

            _PaymentGatewayDbContext = new PaymentGatewayDbContext(options);
        }

        private TransactionRepository CreateTransactionRepository()
        {
            return new TransactionRepository(
                this._PaymentGatewayDbContext);
        }

        [Test]
        public async Task GetTransactionsByMerchantIdAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var transactionRepository = this.CreateTransactionRepository();
            transaction.CardDetails.CardNumber = "5295650000000011";

            await transactionRepository.AddAsync(transaction);
            long merchantId = 22;

            // Act
            var result = await transactionRepository.GetTransactionsByMerchantIdAsync(merchantId);

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result.Count > 0);
        }

        [Test]
        public async Task AddAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            var transactionRepository = this.CreateTransactionRepository();

            // Act
            var result = await transactionRepository.AddAsync(transaction);

            // Assert
            Assert.True(result > 0);
        }

        [Test]
        public async Task UpdateStatusAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            transaction.CardDetails.CardNumber = "5295650000000011";

            var transactionRepository = this.CreateTransactionRepository();
            long transactionId = await transactionRepository.AddAsync(transaction);

            int transactionStatus = 2;

            // Act
            Assert.DoesNotThrowAsync(async () =>
            {
                await transactionRepository.UpdateStatusAsync(
                                                        transactionId,
                                                        transactionStatus);
            });           
        }
    }
}
