using MediatR;
using Nota.Application.Features.Notes.Dtos;

namespace Nota.Application.Features.Notes.Queries.GetById
{
    public class GetNoteByIdQuery : IRequest<NoteDto>
    {
        public int Id { get; set; }
    }
}