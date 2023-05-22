using FlashCards.Models;
using FlashCards.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Controllers
{
    [ApiController]
    [Route("api/CardList/{listId}/[controller]")]
    public class CardsController : Controller
    {

        private ICardListRepository repository;

        public CardsController(ICardListRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public IActionResult GetAllCards(long listId)
        {
            var cards = repository.GetCards(listId);
            return cards != null ? Ok(cards) : NotFound(cards);
        }

        [HttpGet("{id}")]
        public IActionResult GetCardById(long listId, long id)
        {
            var card = repository
                .GetCards(listId)?
                .FirstOrDefault(card => card.Id == id);
            return card != null ? Ok(card) : NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCard(long listId, long id, Card card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var updatedCard = repository.UpdateCard(listId, id, card);
            return updatedCard != null
                ? Ok(updatedCard)
                : NotFound();
        }

        [HttpPost]
        public IActionResult AddCard(long listId, Card card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var createdCard = repository.InsertCard(listId, card);
            return createdCard != null
                ? CreatedAtAction(nameof(GetCardById),
                    new { listId = listId, id = createdCard.Id },
                    createdCard)
                : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveCard(long listId, long id)
        {
            var cardToDelete = repository
                .GetCards(listId)?
                .FirstOrDefault(card => card.Id == id);
            if (cardToDelete != null)
            {
                repository.DeleteCard(cardToDelete);
                return Ok();
            }
            return NotFound();
        }
    }
}