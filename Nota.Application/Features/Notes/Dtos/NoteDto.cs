using Nota.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Nota.Application.Features.Notes.Dtos
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Color { get; set; }

        [EnumDataType(typeof(Priority))]
        public Priority Priority { get; set; }
    }
}