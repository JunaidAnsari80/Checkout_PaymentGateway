using CardService.Application.Commands;
using CardService.Application.Queries;
using CardService.Application.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.API.Models;
using PaymentService.Application.Commands;
using PaymentService.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TransactionService.Application.Commands;
using TransactionService.Application.Queries;
using TransactionService.Application.ViewModel;

namespace PaymentGateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Make a payment, Save card details if not exists in db, save tx in db, make a 3rd party call, update tx status from payment response
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateTransactionRequest request)
        {
            //save card details if not saved before
            var cardDetails = await SaveCardDetails(request);

            // add transaction to store
            var transactionID = await CreateTransaction(request, cardDetails.CardDetailsID);

            //make payment
            var paymentResponse = await MakeAPayment(request);

            //Should be call in result of domain event from MakeAPayment
            await UpdatePaymentStatus(transactionID, paymentResponse);

            return Ok();
        }

        /// <summary>
        /// Return collection of merchant transactions
        /// </summary>
        /// <param name="MerchantID"></param>
        /// <returns></returns>
        [Route("{MerchantID:long}")]
        [HttpGet]
        [ProducesResponseType(typeof(List<TransactionViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(long MerchantID)
        {
            var transactions = await _mediator.Send(new GetTransactionsByMerchantIDQuery() { MerchantID = MerchantID });

            if (transactions.Count == 0)
                return NotFound();

            return Ok(transactions);
        }

        #region Private Methods

        /// <summary>
        /// Updatess payment status in store 
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="paymentResponse"></param>
        /// <returns></returns>
        private async Task UpdatePaymentStatus(long transactionID, PaymentResponseViewModel paymentResponse)
        {
            await _mediator.Send(new UpdateTransactionStatusCommand { TransactionId = transactionID, TransactionStatus = paymentResponse.TransactionStatus });
        }

        /// <summary>
        /// Make a payment and return payment response
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private async Task<PaymentResponseViewModel> MakeAPayment(CreateTransactionRequest request)
        {
            return await _mediator.Send(new MakePaymentCommand
            {
                Amount = request.Amount,
                CardCurrency = request.CardCurrency,
                CardNumber = request.CardNumber,
                Cvv = request.Cvv,
                ExpiryMonth = request.ExpiryMonth,
                ExpiryYear = request.ExpiryYear,
                FirstLineOfAddress = request.FirstLineOfAddress,
                HolderName = request.CardHolderName,
                IdempotentID = request.IdempotentID,
                MerchantID = request.MerchantID,
                PostCode = request.PostCode
            });
        }

        /// <summary>
        /// Create Transaction and return tx id
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private Task<long> CreateTransaction(CreateTransactionRequest request, long CardID)
        {
            var command = new CreateTransactionCommand
            {
                Amount = request.Amount,
                FirstLineOfAddress = request.FirstLineOfAddress,
                ExpiryYear = request.ExpiryYear,
                ExpiryMonth = request.ExpiryMonth,
                CardHolderName = request.CardHolderName,
                IdempotentID = request.IdempotentID,
                MerchantID = request.MerchantID,
                PostCode = request.PostCode,
                CardCurrency = request.CardCurrency,
                CardNumber = request.CardNumber,
                CardID = CardID,
            };
            return _mediator.Send(command);
        }

        /// <summary>
        /// Save card details and return object
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<CardDetailsViewModel> SaveCardDetails(CreateTransactionRequest request)
        {
            var cardDetails = await _mediator.Send(new GetCardDetailsQuery { CardNumber = request.CardNumber });

            if (cardDetails == null)
            {
                cardDetails = await _mediator.Send(new CreateCardDetailsCommand
                {
                    CardCurrency = request.CardCurrency,
                    CardNumber = request.CardNumber,
                    Cvv = request.Cvv,
                    ExpiryMonth = request.ExpiryMonth,
                    ExpiryYear = request.ExpiryYear,
                    FirstLineOfAddress = request.FirstLineOfAddress,
                    CardHolderName = request.CardHolderName,
                    PostCode = request.PostCode
                });

                if (cardDetails == null)
                {
                    throw new Exception("Failed to save card details");
                }
            }
            return cardDetails;
        }

        #endregion
    }
}
