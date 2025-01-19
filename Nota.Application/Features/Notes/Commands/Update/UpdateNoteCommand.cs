using MediatR;
using Nota.Application.Features.Notes.Dtos;

namespace Nota.Application.Features.Notes.Commands.Update
{
    public class UpdateNoteCommand : NoteDto, IRequest<int>
    {
    }
}