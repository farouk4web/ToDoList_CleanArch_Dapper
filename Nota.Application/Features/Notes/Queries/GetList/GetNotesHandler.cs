using MediatR;
using Nota.Application.Common;
using Nota.Application.Features.Notes.Dtos;
using Nota.Application.Interfaces;
using Nota.Domain.Entities;

namespace Nota.Application.Features.Notes.Queries.GetList
{
    public class GetNotesHandler : IRequestHandler<GetNotesQuery, PaginatedListResponse<NoteDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetNotesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedListResponse<NoteDto>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.Repository<Note>().GetAllAsync(request, request.IsDeleted);

            return new PaginatedListResponse<NoteDto>(
                data: NoteDto.ToDto(response.Data),
                totalCount: response.TotalCount,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize
            );
        }
    }

}