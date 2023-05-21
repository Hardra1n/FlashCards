using FlashCards.Models;
using FlashCards.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : Controller
    {

        private ICardListRepository repository;

        public CardsController(ICardListRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public IActionResult GetAllCards()
        {
            // return Ok(repository.Cards);
            return Ok(repository.CardLists.SelectMany(list => list.Cards));
        }

        [HttpGet("{id}")]
        public IActionResult GetCardById(long id)
        {
            var card = repository.CardLists
                .SelectMany(list => list.Cards)
                .FirstOrDefault(card => card.Id == id);
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
            var createdCard = repository.InsertCard(card);
            return createdCard != null
                ? CreatedAtAction(nameof(GetCardById),
                    new { id = createdCard.Id },
                    createdCard)
                : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveCard(long id)
        {
            var cardToDelete = repository.CardLists
                .SelectMany(list => list.Cards)
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