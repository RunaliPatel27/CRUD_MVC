
namespace Sample_CRUD.Models
{
    public class UpdateBooksModel
    {
        public Guid Id { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }

        public int BookPages { get; set; }

    }
}
