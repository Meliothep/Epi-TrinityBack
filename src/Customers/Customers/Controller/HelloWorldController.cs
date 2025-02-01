using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;

namespace Customers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class HelloWorldController : ControllerBase
    {
        // 
        // GET: /HelloWorld/Welcome/ 
        [HttpGet(Name = "Welcome")]
        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}

