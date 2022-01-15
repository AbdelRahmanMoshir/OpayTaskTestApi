using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OpayTaskTestApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ClientOrderController : ControllerBase
    {
        [HttpPost]
        public Task<opay_request> PostJsonAsync(opay_request postParameters)
        {
            string uri = "https://sandboxapi.opaycheckout.com/api/v1/international/cashier/create";
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "OPAYPUB16388855997950.39843277853359504");

            string postData = JsonConvert.SerializeObject(postParameters);
            byte[] bytes = Encoding.UTF8.GetBytes(postData);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentLength = bytes.Length;
            httpWebRequest.ContentType = "text/json";

            using (Stream requestStream = httpWebRequest.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Count());
            }
            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string responseMessge = string.Format("Successful", httpWebResponse);
            if (httpWebResponse.StatusCode != HttpStatusCode.OK)
            {
                string message = string.Format("GET failed. Received HTTP {0}", httpWebResponse.StatusCode);
                throw new ApplicationException(message);
            }

            opay_request request = JsonConvert.DeserializeObject<opay_request>(postData);

            return Task.FromResult(request);
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
}

