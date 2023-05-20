using FlashCards.Models;
using FlashCards.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardListController : Controller
    {
        private ICardListRepository repository;

        public CardListController(ICardListRepository repo)
        {
            this.repository = repo;
        }

        [HttpGet]
        public IActionResult GetAllLists()
        {
            return Ok(repository.CardLists);
        }

        [HttpGet("{id}")]
        public IActionResult GetListById(long id)
        {
            var listToShow = repository.CardLists
                .FirstOrDefault(list => list.Id == id);
            return listToShow != default(CardList)
                ? Ok(listToShow)
                : NotFound();
        }

        [HttpPost]
        public IActionResult CreateList(CardList list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createdList = repository.InsertCardList(list);
            return CreatedAtAction(nameof(GetListById),
                new { id = createdList.Id },
                createdList);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateList(long id, CardList list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var updatedList = repository.UpdateCardList(id, list);

            return updatedList != default(CardList)
                ? Ok(updatedList)
                : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveList(long id)
        {
            var listToRemove = repository.CardLists.FirstOrDefault(list => list.Id == id);
            if (listToRemove != default(CardList))
            {
                repository.DeleteCardList(listToRemove);
                return Ok();
            }
            return NotFound();
        }
    }
}