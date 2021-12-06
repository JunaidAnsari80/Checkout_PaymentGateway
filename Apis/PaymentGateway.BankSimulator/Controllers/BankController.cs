using Microsoft.AspNetCore.Mvc;
using PaymentGateway.BankSimulator.Models;

namespace PaymentGateway.BankSimulator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {       
        [HttpPost]
        public IActionResult Post([FromBody] PaymentRequest request)
        {
            var response = new PaymentResponse();

            switch (request.IdempotentID)
            {
                case "1A":
                    response.Description = "Successful";
                    response.ResponseId = 1;
                    response.TransactionStatus = 2;
                    break;
                case "1B":
                    response.Description = "Rejected";
                    response.ResponseId = 2;
                    response.TransactionStatus = 3;
                    break;
            }

            return Ok(response);
        }
    }
}
