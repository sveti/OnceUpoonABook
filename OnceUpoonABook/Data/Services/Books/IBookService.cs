using OnceUpoonABook.Data.Base;
using OnceUpoonABook.Data.ViewModels;
using OnceUpoonABook.Models;

namespace OnceUpoonABook.Data.Services.Books
{
    public interface IBookService : IEntityBaseRepository<Book>
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int id);
        Book Add(AddBookViewModel addBookViewModel);
        void UpdateBook(EditBookViewModel bookViewModel);
        void DeleteBook(int id);
        IEnumerable<Book> GetBooksByAuthorId(int authorId);

    }
}
