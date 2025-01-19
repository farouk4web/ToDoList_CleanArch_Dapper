using MediatR;
using Nota.Application.Features.Notes.Dtos;

namespace Nota.Application.Features.Notes.Commands.Create
{
    public class CreateNoteCommand : NoteDto, IRequest<int>
    {
    }
}