namespace CRUD_REST_API.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int PublishedYear { get; set; }
        public int AuthorId {  get; set; }
        public decimal? Price { get; set; }
        public string? ImageUrl {  get; set; }
        public Author Author { get; set; } = null!;
        public ICollection<BookCategory> BookCategories { get; set; } = [];
    }
}
