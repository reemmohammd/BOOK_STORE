namespace BOOK_STORE.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Author Author { get; set; }
    }
}
