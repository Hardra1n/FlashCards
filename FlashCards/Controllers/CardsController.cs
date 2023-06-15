using FlashCards.Models;
using FlashCards.Models.Repositories;
using FlashCards.RpcClients;
using FlashCards.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Controllers
{
    [ApiController]
    [Route("api/CardList/{listId}/[controller]")]
    public class CardsController : Controller
    {

        private ICardListService _service;

        public CardsController(ICardListService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCards(long listId)
        {
            var cards = await _service.GetCards(listId);
            return cards != null ? Ok(cards) : NotFound(cards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCardById(long listId, long id)
        {
            var card = await _service.GetCardById(listId, id);
            return card != null ? Ok(card) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCard(long listId, long id, Card card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var updatedCard = await _service.UpdateCard(listId, id, card);
            return updatedCard != null
                ? Ok(updatedCard)
                : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddCard(long listId, Card card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var createdCard = await _service.CreateCard(listId, card);
            return createdCard != null
                ? CreatedAtAction(nameof(GetCardById),
                    new { listId = listId, id = createdCard.Id },
                    createdCard)
                : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCard(long listId, long id)
        {
            var cardToDelete = await _service.GetCardById(listId, id);
            if (cardToDelete != null)
            {
                await _service.RemoveCard(cardToDelete);
                return Ok();
            }
            return NotFound();
        }
    }
}