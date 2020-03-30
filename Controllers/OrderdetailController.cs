using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrderDetail.Controllers
{
    [Route("orderdetails")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet("{id}")]
        public ActionResult<Orderdetail> Get(int id)
        {
            Orderdetail orderdetail = new Orderdetail();
            string userapi = (Environment.GetEnvironmentVariable("userapi")!=null ? Environment.GetEnvironmentVariable("userapi"): "https://localhost:1234/");
            string orderapi = (Environment.GetEnvironmentVariable("orderapi")!=null ? Environment.GetEnvironmentVariable("orderapi"): "https://localhost:2345/");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(userapi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("user/1").Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    orderdetail.UserDetails = response.Content.ReadAsAsync<User>().Result;
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(orderapi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("orders/1").Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    orderdetail.Orders = response.Content.ReadAsAsync<IEnumerable<Order>>().Result;
                }
            }

            return Ok(orderdetail);
        }
    }
}
