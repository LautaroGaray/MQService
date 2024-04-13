using Microsoft.AspNetCore.Mvc;

namespace MQServiceSender.Controllers
{
    [ApiController]
    public class MQController : Controller
    {
        [HttpPost("SendMessage")]
        public IActionResult Send()
        {
            return View();
        }
    }
}
