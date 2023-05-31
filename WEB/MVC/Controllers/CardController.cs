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

        public ActionResult Editor([FromRoute(Name = "id")] long listId, [FromForm] Card card)
        {
            CardViewModel viewModel = new() { listId = listId };
            viewModel.Card = card.Id == default(long) ? null : card;
            return View(viewModel);
        }

        public async Task<ActionResult> Create(
                [FromRoute(Name = "id")] long listId,
                [FromForm] Card card)
        {
            await _client.CreateCard(listId, card);
            return RedirectToAction(nameof(Index), new { id = listId });
        }

        public async Task<ActionResult> Update(
            [FromRoute(Name = "id")] long listId,
            [FromQuery(Name = "card-id")] long cardId,
            [FromForm] Card card)
        {
            await _client.UpdateAsyncCard(listId, cardId, card);
            return RedirectToAction(nameof(Index), new { id = listId });
        }

        public async Task<ActionResult> Delete(
            [FromRoute(Name = "id")] long listId,
            [FromQuery(Name = "card-id")] long cardId)
        {
            await _client.DeleteAsyncCard(listId, cardId);
            return RedirectToAction(nameof(Index), new { id = listId });
        }
    }
}