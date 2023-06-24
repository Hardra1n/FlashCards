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
        private RepetitionApiService _service;

        public RepititionController(RepetitionApiService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync() => Ok(await _service.GetAllRepetitions());

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var result = await _service.GetRepetitionById(id);
            return result != null ? Ok(result.ToReadDto()) : NotFound();
        }

        // [HttpPost]
        // public async Task<ActionResult> Create()
        // {
        //     var result = await _service.CreateRepetition();
        //     return result != null
        //         ? CreatedAtAction(nameof(GetById), new { id = result.Id }, result)
        //         : BadRequest();
        // }

        [HttpPut]
        public async Task<ActionResult> Update(RepetitionUpdateDto dto)
        {
            var result = await _service.UpdateRepetition(dto.ToRepetition());
            return result != null ? Ok(result) : NotFound();
        }

        // [HttpDelete("{id}")]
        // public async Task<ActionResult> Delete(long id)
        //     => await _service.RemoveRepetition(id) ? Ok() : NotFound();
    }
}