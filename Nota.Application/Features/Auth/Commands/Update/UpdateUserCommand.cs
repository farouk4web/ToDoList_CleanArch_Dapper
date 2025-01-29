using MediatR;
using Nota.Application.Features.Auth.Dtos;

namespace Nota.Application.Features.Auth.Commands.Create
{
    public class UpdateUserCommand : UserDto, IRequest<int>
    {
    }
}