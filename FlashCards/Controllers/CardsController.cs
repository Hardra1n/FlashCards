using FlashCards.Models;
using FlashCards.Models.Dtos;
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

        private CardListApiService _service;

        public CardsController(CardListApiService service)
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
            var getCardDto = await _service.GetCardById(listId, id);
            return getCardDto != null ? Ok(getCardDto) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCard(long listId, long id, UpdateCardDto cardDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var updatedCard = await _service.UpdateCard(listId, id, cardDto.ToCard());
            return updatedCard != null
                ? Ok(updatedCard)
                : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddCard(long listId, CreateCardDto cardDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var getCardDto = await _service.CreateCard(listId, cardDto.ToCard());
            return getCardDto != null
                ? CreatedAtAction(nameof(GetCardById),
                    new { listId = listId, id = getCardDto.Id },
                    getCardDto)
                : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCard(long listId, long id)
        {
            return await _service.RemoveCard(listId, id) ? Ok() : NotFound();
        }
    }
}