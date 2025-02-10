using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace Customers.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class HelloWorldController : ControllerBase
{
    // 
    // GET: /HelloWorld/
    [HttpGet]
    public string Index()
    {
        return "This is my default action...";
    }

}