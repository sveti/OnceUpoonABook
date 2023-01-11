using OnceUpoonABook.Data.Base;
using OnceUpoonABook.Data.ViewModels;
using OnceUpoonABook.Models;

namespace OnceUpoonABook.Data.Services
{
    public interface IBookService: IEntityBaseRepository<Book>
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int id);

        Book Add(AddBookViewModel addBookViewModel);
    }
}
