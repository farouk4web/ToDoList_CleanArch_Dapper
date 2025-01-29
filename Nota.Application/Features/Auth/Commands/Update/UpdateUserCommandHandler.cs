using MediatR;
using Nota.Application.Features.Auth.Commands.Create;
using Nota.Application.Interfaces;
using Nota.Application.Mapping;
using Nota.Domain.Entities;

namespace Nota.Application.Features.Auth.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            // TODO: Before Update add UserType And HashPassowrd

            return await _unitOfWork.Repository<User>().AddAsync(request.ToDomain());
        }
    }
}