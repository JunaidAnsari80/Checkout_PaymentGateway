using CardService.Application.Commands;
using CardService.Application.Queries;
using CardService.Application.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PaymentGateway.API.Controllers;
using PaymentGateway.API.Models;
using PaymentService.Application.Commands;
using PaymentService.Application.ViewModel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TransactionService.Application.Commands;
using TransactionService.Application.Queries;
using TransactionService.Application.ViewModel;

namespace PaymentGateway.API.Tests.Controllers
{
    [TestFixture]
    public class TransactionControllerTests
    {
        private Mock<IMediator> _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = new Mock<IMediator>();
        }

        private TransactionController CreateTransactionController()
        {
            return new TransactionController(
                _mediator.Object);
        }

        [Test]
        public async Task Post_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sut = this.CreateTransactionController();
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);
            CreateTransactionRequest request = new CreateTransactionRequest
            {
                CardCurrency = "GBP",
                CardHolderName = "JamesBond",
                CardNumber = "5295650000000000",
                Cvv = "333",
                ExpiryMonth = 10,
                ExpiryYear = 2020,
                FirstLineOfAddress = "adr",
                PostCode = "PC",
                Amount = 10,
                IdempotentID = "1A",
                MerchantID = 20
            };

            _mediator.Setup(x => x.Send(It.IsAny<GetCardDetailsQuery>(), cancellationToken)).ReturnsAsync(default(CardDetailsViewModel));

            _mediator.Setup(x => x.Send(It.IsAny<CreateCardDetailsCommand>(), cancellationToken)).ReturnsAsync(new CardDetailsViewModel() { CardDetailsID = 1 });

            _mediator.Setup(x => x.Send(It.IsAny<CreateTransactionCommand>(), cancellationToken)).ReturnsAsync(1);

            _mediator.Setup(x => x.Send(It.IsAny<MakePaymentCommand>(), cancellationToken)).ReturnsAsync(new PaymentResponseViewModel { ResponseId = 1, Description = "succcess", TransactionStatus = 1 });

            _mediator.Setup(x => x.Send(It.IsAny<UpdateTransactionStatusCommand>(), cancellationToken));

            // Act
            var result = await sut.Post(
                request);

            // Assert           
            _mediator.Verify(x => x.Send(It.IsAny<GetCardDetailsQuery>(), cancellationToken), Times.Once);

            _mediator.Verify(x => x.Send(It.IsAny<CreateCardDetailsCommand>(), cancellationToken), Times.Once);

            _mediator.Verify(x => x.Send(It.IsAny<CreateTransactionCommand>(), cancellationToken), Times.Once);

            _mediator.Verify(x => x.Send(It.IsAny<MakePaymentCommand>(), cancellationToken), Times.Once);

            _mediator.Verify(x => x.Send(It.IsAny<UpdateTransactionStatusCommand>(), cancellationToken), Times.Once);

        }

        [Test]
        public async Task Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sut = this.CreateTransactionController();
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);
            _mediator.Setup(x => x.Send(It.IsAny<GetTransactionsByMerchantIDQuery>(), cancellationToken)).ReturnsAsync(new List<TransactionViewModel> { new TransactionViewModel() });
            long MerchantID = 1;

            // Act
            var result = await sut.Get(
                MerchantID);
            // Assert         
            Assert.That(result is OkObjectResult);
            _mediator.Verify(x => x.Send(It.IsAny<GetTransactionsByMerchantIDQuery>(), cancellationToken), Times.Once);
        }

        [Test]
        public async Task Get_StateUnderTest_UnepectedBehavior()
        {
            // Arrange
            var sut = this.CreateTransactionController();
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);
            _mediator.Setup(x => x.Send(It.IsAny<GetTransactionsByMerchantIDQuery>(), cancellationToken)).ReturnsAsync(new List<TransactionViewModel> ());
            long MerchantID = 1;

            // Act
            var result = await sut.Get(
                MerchantID);
            // Assert         
            Assert.That(result is NotFoundResult);
            _mediator.Verify(x => x.Send(It.IsAny<GetTransactionsByMerchantIDQuery>(), cancellationToken), Times.Once);
        }
    }
}
