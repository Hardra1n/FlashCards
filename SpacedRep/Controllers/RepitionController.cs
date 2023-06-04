using Microsoft.AspNetCore.Mvc;
using SpacedRep.Data;
using SpacedRep.Models;

namespace SpacedRep.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepititionController : ControllerBase
    {
        private IRepetitionRepository _repo;

        public RepititionController(IRepetitionRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult GetAll() => Ok(_repo.Read());

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var result = await _repo.ReadAsync(id);
            return result != null ? Ok(result.ToReadDto()) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create()
        {
            var result = await _repo.CreateAsync();
            return result != null
                ? CreatedAtAction(nameof(GetById), new { id = result.Id }, result)
                : BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> Update(RepetitionUpdateDto dto)
        {
            var result = await _repo.UpdateAsync(dto.ToRepetition());
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
            => await _repo.DeleteAsync(id) ? Ok() : NotFound();
    }
}