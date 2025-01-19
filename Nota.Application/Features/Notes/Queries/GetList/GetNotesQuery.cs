using MediatR;
using Nota.Application.Common;
using Nota.Application.Features.Notes.Dtos;

namespace Nota.Application.Features.Notes.Queries.GetList
{
    public class GetNotesQuery : PaginatedRequest, IRequest<PaginatedListResponse<NoteDto>>
    {
        public bool IsDeleted { get; set; }
    }
}