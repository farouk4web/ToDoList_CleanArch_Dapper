﻿using MediatR;
using Nota.Application.Interfaces;
using Nota.Domain.Entities;

namespace Nota.Application.Features.Notes.Commands.Delete
{
    public class ToggleDeleteNoteCommandHandler : IRequestHandler<ToggleDeleteNoteCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ToggleDeleteNoteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(ToggleDeleteNoteCommand request, CancellationToken cancellationToken)
        {
            bool isExist = await _unitOfWork.Repository<Note>().IsExistAsync(request.Id);
            if (!isExist)
                throw new KeyNotFoundException("Note not found");

            return await _unitOfWork.Repository<Note>().ToggleDeleteAsync(request.Id);
        }
    }
}
