namespace CRUD_REST_API.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public int PublishedYear { get; set; }
        public int AuthorId {  get; set; }
        public Author Author { get; set; } = null!;
    }
}
