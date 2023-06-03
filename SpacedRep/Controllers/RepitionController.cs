using Microsoft.AspNetCore.Mvc;
using SpacedRep.Data;

namespace SpacedRep.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepititionController : ControllerBase
    {
        private SpacedRepDbContext _context;

        public RepititionController(SpacedRepDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(_context.Repititions.AsEnumerable());
        }
    }
}