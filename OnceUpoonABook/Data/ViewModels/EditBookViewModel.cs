using OnceUpoonABook.Models;

namespace OnceUpoonABook.Data.ViewModels
{
    public class EditBookViewModel
    {
        public Book Book { get; set; }
        public List<int> AuthorIds { get; set; } = new List<int>();
    }
}
