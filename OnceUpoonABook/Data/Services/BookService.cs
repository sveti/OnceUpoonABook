using Microsoft.EntityFrameworkCore;
using OnceUpoonABook.Data.Base;
using OnceUpoonABook.Data.ViewModels;
using OnceUpoonABook.Models;

namespace OnceUpoonABook.Data.Services
{
    public class BookService : EntityBaseRepository<Book>, IBookService
    {
        private readonly AppDBContext appDBContext;

        public BookService(AppDBContext appDBContext) : base(appDBContext) {
            this.appDBContext = appDBContext;
        }

        public Book Add(AddBookViewModel addBookViewModel)
        {
            var book = new Book(addBookViewModel);
            var addedBook =  Add(book);

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

        public IEnumerable<Book> GetAllBooks()
        {
            return appDBContext.Books
                .Include(item => item.Publisher)
                .Include(item => item.Author_Book)
                .ThenInclude(authorbook => authorbook.Author).ToList();
        }

        public Book GetBookById(int id)
        {
            return appDBContext.Books.Include(item => item.Publisher).Include(item => item.Author_Book).ThenInclude(authorbook => authorbook.Author).FirstOrDefault( item=> item.Id == id);
        }
    }
   
}
