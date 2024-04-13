using LogicLayer.Handlers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MQBusService.Controllers
{
    [ApiController]
    public class EventBusController : Controller
    {
        private readonly IMQHandler _mqHandler;

        public EventBusController(IMQHandler mqHandler)
        {
            _mqHandler = mqHandler;
        }
        [HttpPost]
        public IActionResult Routing()
        {
            return View();
        }
    }
}
