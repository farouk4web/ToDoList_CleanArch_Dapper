using Nota.Application.Features.Notes.Dtos;
using Nota.Domain.Entities;

namespace Nota.Application.Mapping
{
    public static class NoteMapping
    {
        public static Note ToDomain(this NoteDto dto)
        {
            return new Note
            {
                Id = dto.Id,
                Color = dto.Color,
                Title = dto.Title,
                Content = dto.Content,
                Priority = dto.Priority,
            };
        }

        public static NoteDto ToDto(this Note domain)
        {
            return new NoteDto
            {
                Id = domain.Id,
                Color = domain.Color,
                Title = domain.Title,
                Content = domain.Content,
                Priority = domain.Priority,
            };
        }
    }
}