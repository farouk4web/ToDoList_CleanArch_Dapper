namespace Nota.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? HashPassword { get; set; }
    }
}