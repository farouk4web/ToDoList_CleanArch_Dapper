using MediatR;
using Nota.Application.Features.Notes.Dtos;
using Nota.Application.Interfaces;
using Nota.Application.Mapping;
using Nota.Domain.Entities;

namespace Nota.Application.Features.Notes.Queries.GetById
{
    public class GetNoteByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetNoteByIdQuery, NoteDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<NoteDto> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
        {
            var entityInDb = await _unitOfWork.Repository<Note>().GetByIdAsync(request.Id);
            if (entityInDb == null)
                throw new KeyNotFoundException("Note not found");

            return entityInDb.ToDto();
        }
    }
}