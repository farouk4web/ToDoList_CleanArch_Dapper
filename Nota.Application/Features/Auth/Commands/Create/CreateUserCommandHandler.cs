using MediatR;
using Nota.Application.Interfaces;
using Nota.Application.Mapping;
using Nota.Domain.Entities;

namespace Nota.Application.Features.Auth.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // TODO: Before Insert add UserType And HashPassowrd

            return await _unitOfWork.Repository<User>().AddAsync(request.ToDomain());
        }
    }
}