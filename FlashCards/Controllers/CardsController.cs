using FlashCards.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : Controller
    {
        private ICardRepository repository;

        public CardsController(ICardRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public IActionResult GetAllCards()
        {
            return Ok(repository.Cards);
        }

        [HttpGet("{id}")]
        public IActionResult GetCardById(long id)
        {
            var card = repository.Cards.FirstOrDefault(card => card.Id == id);
            return card != null ? Ok(card) : NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCard(long id, Card card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var updatedCard = repository.UpdateCard(id, card);
            return updatedCard != null
                ? Ok(updatedCard)
                : NotFound();
        }

        [HttpPost]
        public IActionResult AddCard(Card card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var createdCard = repository.AddCard(card);
            return CreatedAtAction(nameof(GetCardById),
                new { id = createdCard.Id },
                createdCard);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCard(long id)
        {
            var cardToDelete = repository
                .Cards.FirstOrDefault(card => card.Id == id);
            if (cardToDelete != null)
            {
                repository.RemoveCard(cardToDelete);
                return Ok();
            }
            return NotFound();
        }
    }
}