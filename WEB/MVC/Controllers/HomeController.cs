using Microsoft.AspNetCore.Mvc;

namespace FlashCards.WEB.MVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [Route("/Index")]
        public async Task<ViewResult> Index()
        {
            return View();
        }
    }
}