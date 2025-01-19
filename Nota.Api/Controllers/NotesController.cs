using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nota.Application.Features.Notes.Commands.Create;
using Nota.Application.Features.Notes.Commands.Delete;
using Nota.Application.Features.Notes.Commands.Update;
using Nota.Application.Features.Notes.Queries.GetById;
using Nota.Application.Features.Notes.Queries.GetList;

namespace Nota.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetNotesQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetNoteByIdQuery() { Id = id });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNoteCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    error = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateNoteCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    error = ex.Message
                });
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> ToggleDelete(int id)
        {
            try
            {
                var result = await _mediator.Send(new ToggleDeleteNoteCommand() { Id = id });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    error = ex.Message
                });
            }
        }
    }
}
