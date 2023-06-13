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
        public async Task<IActionResult> GetAllLists()
        {
            return Ok(await _service.GetCardLists());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetListById(long id)
        {
            var listToShow = await _service.GetCardListById(id);
            return listToShow != default(CardList)
                ? Ok(listToShow)
                : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateList(CardList list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createdList = await _service.CreateCardList(list);
            return createdList != default(CardList)
                ? CreatedAtAction(nameof(GetListById),
                    new { id = createdList.Id },
                    createdList)
                : BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateList(long id, CardList list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var updatedList = await _service.UpdateCardList(id, list);

            return updatedList != default(CardList)
                ? Ok(updatedList)
                : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveList(long id)
        {
            var listToRemove = await _service.GetCardListById(id);
            if (listToRemove != default(CardList))
            {
                _service.RemoveCardList(listToRemove);
                return Ok();
            }
            return NotFound();
        }
    }
}