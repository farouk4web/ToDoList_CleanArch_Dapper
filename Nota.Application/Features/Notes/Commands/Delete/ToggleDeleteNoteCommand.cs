using MediatR;

namespace Nota.Application.Features.Notes.Commands.Delete
{
    public class ToggleDeleteNoteCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}