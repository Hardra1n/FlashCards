using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : Controller
    {
        ICardRepository repository;

        public CardsController(ICardRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public IActionResult GetAllCards()
        {
            return Json(repository.Cards.FirstOrDefault());
        }
    }
}