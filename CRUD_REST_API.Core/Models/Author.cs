namespace CRUD_REST_API.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Biography { get; set; } = null!;
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
