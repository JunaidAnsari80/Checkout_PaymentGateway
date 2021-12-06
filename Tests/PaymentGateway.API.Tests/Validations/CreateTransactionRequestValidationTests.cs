using NUnit.Framework;
using PaymentGateway.API.Models;
using PaymentGateway.API.Validations;
using System.Linq;

namespace PaymentGateway.API.Tests.Validations
{
    [TestFixture]
    public class CreateTransactionRequestValidationTests
    {
        private CreateTransactionRequest _testRequest;

        [SetUp]
        public void SetUp()
        {
            _testRequest = new CreateTransactionRequest()
            {
                Amount = 10,
                CardCurrency = "GBP",
                CardHolderName = "JamesBond",
                CardNumber = "5295650000000000",
                Cvv = "222",
                ExpiryMonth = 10,
                ExpiryYear = 2020,
                FirstLineOfAddress = "address",
                IdempotentID = "1A",
                MerchantID = 22,
                PostCode = "DA15"
            };
        }

        [Test]
        public void Validation_StateUnderTest_ExpectedBehavior()
        {
            //arrange
            var validator = new CreateTransactionRequestValidation();
            
            //Act
            var result = validator.Validate(_testRequest);

            //Assert
            Assert.That(result.IsValid);
        }


        [Test]
        public void Should_have_error_when_amount_is_zero()
        {
            //arrange
            var validator = new CreateTransactionRequestValidation();
            _testRequest.Amount = 0;
            //Act
            var result = validator.Validate(_testRequest);
            
            //Assert
            Assert.That(result.Errors.Any(o => o.PropertyName == "Amount"));
        }

        [Test]
        public void Should_have_error_when_IdempotentID_is_empty()
        {
            //arrange
            var validator = new CreateTransactionRequestValidation();
            _testRequest.IdempotentID = "";
            //Act
            var result = validator.Validate(_testRequest);

            //Assert
            Assert.That(result.Errors.Any(o => o.PropertyName == "IdempotentID"));
        }

        [Test]
        public void Should_have_error_when_MerchantID_is_zero()
        {
            //arrange
            var validator = new CreateTransactionRequestValidation();
            _testRequest.MerchantID = 0;
            //Act
            var result = validator.Validate(_testRequest);

            //Assert
            Assert.That(result.Errors.Any(o => o.PropertyName == "MerchantID"));
        }

        [Test]
        public void Should_have_error_when_CardNumber_is_less_then_16()
        {
            //arrange
            var validator = new CreateTransactionRequestValidation();
            _testRequest.CardNumber = "5550";
            //Act
            var result = validator.Validate(_testRequest);

            //Assert
            Assert.That(result.Errors.Any(o => o.PropertyName == "CardNumber"));
        }

        [Test]
        public void Should_have_error_when_ExpiryMonth_is_zer0()
        {
            //arrange
            var validator = new CreateTransactionRequestValidation();
            _testRequest.ExpiryMonth = 0;
            //Act
            var result = validator.Validate(_testRequest);

            //Assert
            Assert.That(result.Errors.Any(o => o.PropertyName == "ExpiryMonth"));
        }

        [Test]
        public void Should_have_error_when_ExpiryYear_is_zer0()
        {
            //arrange
            var validator = new CreateTransactionRequestValidation();
            _testRequest.ExpiryYear = 0;
            //Act
            var result = validator.Validate(_testRequest);

            //Assert
            Assert.That(result.Errors.Any(o => o.PropertyName == "ExpiryYear"));
        }

        [Test]
        public void Should_have_error_when_FirstLineOfAddress_is_empty()
        {
            //arrange
            var validator = new CreateTransactionRequestValidation();
            _testRequest.FirstLineOfAddress = "";
            //Act
            var result = validator.Validate(_testRequest);

            //Assert
            Assert.That(result.Errors.Any(o => o.PropertyName == "FirstLineOfAddress"));
        }

        [Test]
        public void Should_have_error_when_Postcode_is_empty()
        {
            //arrange
            var validator = new CreateTransactionRequestValidation();
            _testRequest.PostCode = "";
            //Act
            var result = validator.Validate(_testRequest);

            //Assert
            Assert.That(result.Errors.Any(o => o.PropertyName == "PostCode"));
        }
    }
}
