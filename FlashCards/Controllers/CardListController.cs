using FlashCards.Models;
using FlashCards.Models.Dtos;
using FlashCards.Models.Repositories;
using FlashCards.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardListController : Controller
    {
        private CardListApiService _service;

        public CardListController(CardListApiService service)
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
            var cardListDto = await _service.GetCardListById(id);
            return cardListDto != null
                ? Ok(cardListDto)
                : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateList(CreateCardListDto listDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createdList = await _service.CreateCardList(listDto.ToCardList());
            return createdList != default(CardList)
                ? CreatedAtAction(nameof(GetListById),
                    new { id = createdList.Id },
                    createdList)
                : BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateList(long id, UpdateCardListDto listDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var updatedList = await _service.UpdateCardList(id, listDto.ToCardList());

            return updatedList != default(CardList)
                ? Ok(updatedList)
                : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveList(long id)
        {
            return await _service.RemoveCardList(id) ? Ok() : NotFound();
        }
    }
}