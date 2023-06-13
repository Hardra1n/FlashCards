using FlashCards.Models;
using FlashCards.Models.Repositories;
using FlashCards.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardListController : Controller
    {
        private ICardListService _service;

        public CardListController(ICardListService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllLists()
        {
            return Ok(_service.GetCardLists());
        }

        [HttpGet("{id}")]
        public IActionResult GetListById(long id)
        {
            var listToShow = _service.GetCardListById(id);
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

            var createdList = _service.CreateCardList(list);
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
            var updatedList = _service.UpdateCardList(id, list);

            return updatedList != default(CardList)
                ? Ok(updatedList)
                : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveList(long id)
        {
            var listToRemove = _service.GetCardListById(id);
            if (listToRemove != default(CardList))
            {
                _service.RemoveCardList(listToRemove);
                return Ok();
            }
            return NotFound();
        }
    }
}