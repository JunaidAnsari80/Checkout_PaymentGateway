using Moq;
using Moq.Protected;
using NUnit.Framework;
using PaymentService.Application.Commands;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentService.Tests.Application.Commands
{
    [TestFixture]
    public class MakePaymentCommandHandlerTests
    {
        private Mock<IHttpClientFactory> _clientFactory;
        private Mock<DelegatingHandler> _clientHandlerMock;
        private string _content;

        [SetUp]
        public void SetUp()
        {
            _clientHandlerMock = new Mock<DelegatingHandler>();

            _clientHandlerMock.As<IDisposable>().Setup(s => s.Dispose());

            var httpClient = new HttpClient(_clientHandlerMock.Object);

            _clientFactory = new Mock<IHttpClientFactory>(MockBehavior.Strict);

            httpClient.BaseAddress = new Uri("http://www.google.com");

            _clientFactory.Setup(cf => cf.CreateClient(It.IsAny<string>())).Returns(httpClient);

            _content = "{\"responseId\": 1,  \"transactionStatus\": 2,  \"description\": null}";
        }

        [Test]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            _clientHandlerMock.Protected()
                                       .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                                       .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(_content) })
                                       .Verifiable();

            var makePaymentCommandHandler = new MakePaymentCommandHandler(_clientFactory.Object);
            MakePaymentCommand request = new MakePaymentCommand();
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await makePaymentCommandHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            _clientHandlerMock.Protected().Verify("SendAsync", Times.Exactly(1), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
        }

        [Test]
        public async Task Handle_StateUnderTest_UnexpectedBehavior()
        {
            // Arrange
            _clientHandlerMock.Protected()
                                       .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                                       .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.NotFound) { })
                                       .Verifiable();

            var makePaymentCommandHandler = new MakePaymentCommandHandler(_clientFactory.Object);
            MakePaymentCommand request = new MakePaymentCommand();
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await makePaymentCommandHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.IsNull(result);
            _clientHandlerMock.Protected().Verify("SendAsync", Times.Exactly(1), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
        }
    }
}
