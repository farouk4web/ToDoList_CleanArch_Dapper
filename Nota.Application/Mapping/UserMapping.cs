using Nota.Application.Features.Auth.Dtos;
using Nota.Domain.Entities;

namespace Nota.Application.Mapping
{
    public static class UserMapping
    {
        public static User ToDomain(this UserDto dto)
        {
            return new User
            {
                FullName = dto.FullName,
                UserName = dto.UserName,
                HashPassword = dto.Password
            };
        }

        public static UserDto ToDto(this User dto)
        {
            return new UserDto
            {
                FullName = dto.FullName,
                UserName = dto.UserName,
            };
        }
    }
}