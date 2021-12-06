using MediatR;
using Newtonsoft.Json;
using PaymentService.Application.ViewModel;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentService.Application.Commands
{
    public class MakePaymentCommandHandler : IRequestHandler<MakePaymentCommand, PaymentResponseViewModel>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        
        public MakePaymentCommandHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Calls the 3rd party and return the response model
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PaymentResponseViewModel> Handle(MakePaymentCommand request, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient("PaymentService");

            var payload = JsonConvert.SerializeObject(request);

            var response = await client.SendAsync(new HttpRequestMessage { Method = HttpMethod.Post, Content = new StringContent(payload, Encoding.UTF8, "application/json") }, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<PaymentResponseViewModel>(await response.Content.ReadAsStringAsync());
            else
                return null;
        }
    }
}
