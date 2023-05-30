using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Models;

namespace MVC.Controllers
{
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

        public async Task<ActionResult> Create([FromForm] CardList list)
        {
            await _client.CreateAsyncCardList(list);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Update([FromRoute] long id, [FromForm] CardList list)
        {
            await _client.UpdateAsyncCardList(id, list);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(long id)
        {
            await _client.DeleteAsyncCardList(id);
            return RedirectToAction(nameof(Index));
        }
    }
}