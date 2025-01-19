using MediatR;
using Nota.Application.Interfaces;
using Nota.Domain.Entities;

namespace Nota.Application.Features.Notes.Commands.Create
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateNoteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var newNote = new Note()
            {
                Title = request.Title,
                Color = request.Color,
                Content = request.Content
            };

            return await _unitOfWork.Repository<Note>().AddAsync(newNote);
        }
    }
}