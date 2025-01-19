namespace Nota.Domain.Entities
{
    public class Note : BaseEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Color { get; set; }
    }
}