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

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            return View(await _client.GetAsyncCardLists());
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CardList list)
        {
            await _client.CreateAsyncCardList(list);
            return RedirectToAction(nameof(Index));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, CardList list)
        {
            await _client.UpdateAsyncCardList(id, list);
            return RedirectToAction(nameof(Index));
        }
    }
}