using MediatR;
using Nota.Application.Interfaces;
using Nota.Domain.Entities;

namespace Nota.Application.Features.Notes.Commands.Update
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateNoteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            bool isExist = await _unitOfWork.Repository<Note>().IsExistAsync(request.Id);
            if (!isExist)
                throw new KeyNotFoundException("Note not found");

            var updatedNote = new Note()
            {
                Id = request.Id,
                Title = request.Title,
                Color = request.Color,
                Content = request.Content
            };

            return await _unitOfWork.Repository<Note>().UpdateAsync(updatedNote); ;
        }
    }
}
