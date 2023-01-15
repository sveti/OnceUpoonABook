using Microsoft.EntityFrameworkCore;
using OnceUpoonABook.Data.Base;
using OnceUpoonABook.Data.ViewModels;
using OnceUpoonABook.Models;

namespace OnceUpoonABook.Data.Services.Books
{
    public class BookService : EntityBaseRepository<Book>, IBookService
    {
        private readonly AppDBContext appDBContext;

        public BookService(AppDBContext appDBContext) : base(appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public Book Add(AddBookViewModel addBookViewModel)
        {
            var book = new Book(addBookViewModel);
            var addedBook = Add(book);

            foreach (var authorId in addBookViewModel.AuthorIds)
            {
                var authorBook = new Author_Book()
                {
                    BookId = addedBook.Id,
                    AuthorId = authorId
                };
                appDBContext.Authors_Books.Add(authorBook);
            }
            appDBContext.SaveChanges();

            return addedBook;
        }

        public void DeleteBook(int id)
        {

            appDBContext.Authors_Books.RemoveRange(appDBContext.Authors_Books.Where(ab => ab.BookId == id));
            appDBContext.Books.Remove(appDBContext.Books.First(book => book.Id == id));
            appDBContext.SaveChanges();

        }

        public IEnumerable<Book> GetAllBooks()
        {
            return appDBContext.Books
                .Include(item => item.Publisher)
                .Include(item => item.Author_Book)
                .ThenInclude(authorbook => authorbook.Author).ToList();
        }

        public Book GetBookById(int id)
        {
            return appDBContext.Books.Include(item => item.Publisher).Include(item => item.Author_Book).ThenInclude(authorbook => authorbook.Author).FirstOrDefault(item => item.Id == id);
        }

        public void UpdateBook(EditBookViewModel bookViewModel)
        {
            var updatedBook = new Book(bookViewModel);
            appDBContext.Books.Update(updatedBook);

            //remove all relationships
            appDBContext.Authors_Books.RemoveRange(appDBContext.Authors_Books.Where(ab => ab.BookId == updatedBook.Id));

            //add them again
            foreach (var authorId in bookViewModel.AuthorIds)
            {
                var authorBook = new Author_Book()
                {
                    BookId = updatedBook.Id,
                    AuthorId = authorId
                };
                appDBContext.Authors_Books.Add(authorBook);
            }

            appDBContext.SaveChanges();
        }
    }

}
