using Nota.Domain.Entities;

namespace Nota.Application.Features.Notes.Dtos
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Color { get; set; }

        public static NoteDto ToDto(Note note)
        {
            return new NoteDto
            {
                Id = note.Id,
                Color = note.Color,
                Title = note.Title,
                Content = note.Content,
            };
        }

        public static IEnumerable<NoteDto> ToDto(IEnumerable<Note> notes)
            => notes.Select(x => ToDto(x));

    }
}