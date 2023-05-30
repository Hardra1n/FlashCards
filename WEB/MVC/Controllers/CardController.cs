using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Models;

namespace MVC.Controllers
{
    public class CardController : Controller
    {
        private FlashCardsClient _client;

        public CardController(FlashCardsClient client)
        {
            _client = client;
        }

        public async Task<ActionResult> Index([FromRoute(Name = "id")] long listId)
        {
            CardList? list = await _client.GetAsyncCardLists(listId);
            var cards = await _client.GetAsyncCardsByListId(listId);
            if (cards != null && list != null)
            {
                list.Cards = cards;
            }
            return View(list);
        }

        public async Task<ActionResult> Create(
            [FromRoute(Name = "id")] long listId,
            [FromForm] Card card)
        {
            await _client.CreateCard(listId, card);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Update(
            [FromRoute] long listId,
            [FromRoute] long cardId,
            [FromForm] Card card)
        {
            await _client.UpdateAsyncCard(listId, cardId, card);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(
            [FromRoute] long listId,
            [FromRoute] long cardId)
        {
            await _client.DeleteAsyncCard(listId, cardId);
            return RedirectToAction(nameof(Index));
        }
    }
}