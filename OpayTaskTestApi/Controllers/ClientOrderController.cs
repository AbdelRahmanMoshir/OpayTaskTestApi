using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpayTaskTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientOrderController : ControllerBase
    {
        public async Task<opay_request> PostJsonAsync(opay_request opayRequestData)
        {
            string requestUrl = "https://sandboxapi.opaycheckout.com/api/v1/international/cashier/create";

            opay_request opayRequest = new opay_request()
            {
                reference = opayRequestData.reference,
                payMethod = opayRequestData.payMethod,
                returnUrl = opayRequestData.returnUrl,
                callbackUrl = opayRequestData.callbackUrl,
                cancelUrl = opayRequestData.cancelUrl,
                userClientIP = opayRequestData.userClientIP,
                expireAt = opayRequestData.expireAt,
                amount = opayRequestData.amount,
            };

            var newRequest = JsonConvert.SerializeObject(opayRequest);

            var requestContent = new StringContent(newRequest, Encoding.UTF8, "application/json");

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(requestUrl, requestContent);

            response.EnsureSuccessStatusCode();

            string responseData = await response.Content.ReadAsStringAsync();

            opay_request request = JsonConvert.DeserializeObject<opay_request>(responseData);

            return request;
        }
    }

    public class opay_request
    {
        public string reference { get; set; }
        public string payMethod { get; set; }
        public string returnUrl { get; set; }
        public string callbackUrl { get; set; }
        public string cancelUrl { get; set; }
        public string userClientIP { get; set; }
        public string expireAt { get; set; }
        public product product;
        public amount amount;
    }
    public class amount
    {
        public string total { get; set; }
        public string currency { get; set; }
    }
    public class product
    {
        public string name { get; set; }
        public string description { get; set; }
    }
}

