using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Models;

namespace MVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardListController : Controller
    {
        private FlashCardsClient _client;

        public CardListController(FlashCardsClient client)
        {
            _client = client;
        }

        public async Task<ViewResult> Index()
        {
            return View(await _client.GetAsyncCardLists());
        }
    }
}