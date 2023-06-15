using Microsoft.AspNetCore.Mvc;
using SpacedRep.Data;
using SpacedRep.Models;
using SpacedRep.Services;

namespace SpacedRep.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepititionController : ControllerBase
    {
        private IRepetitionService _service;

        public RepititionController(IRepetitionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync() => Ok(await _service.Read());

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var result = await _service.ReadAsync(id);
            return result != null ? Ok(result.ToReadDto()) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create()
        {
            var result = await _service.CreateAsync();
            return result != null
                ? CreatedAtAction(nameof(GetById), new { id = result.Id }, result)
                : BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> Update(RepetitionUpdateDto dto)
        {
            var result = await _service.UpdateAsync(dto.ToRepetition());
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
            => await _service.DeleteAsync(id) ? Ok() : NotFound();
    }
}