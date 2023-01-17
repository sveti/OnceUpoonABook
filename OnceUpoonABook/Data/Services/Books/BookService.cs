using Microsoft.EntityFrameworkCore;
using OnceUpoonABook.Data.Base;
using OnceUpoonABook.Data.ViewModels;
using OnceUpoonABook.Models;
using System.Linq;

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

            appDBContext.CartItems.RemoveRange(appDBContext.CartItems.Where(cartItem => cartItem.BookId == id));
            appDBContext.OrderItems.RemoveRange(appDBContext.OrderItems.Where(orderItem => orderItem.BookId == id));
            appDBContext.SaveChanges();

            var allOrders = appDBContext.Orders.ToList();
            foreach (var order in allOrders)
            {
                //the order doesnt have items
                if(appDBContext.OrderItems.Any(orderItem => orderItem.OrderId == order.Id) == false)
                {
                    appDBContext.Orders.Remove(order);
                }
            }

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

        public IEnumerable<Book> GetBooksByAuthorId(int authorId)
        {
           var authorBooks = appDBContext.Authors_Books
                .Where(ab => ab.AuthorId == authorId).Select(ab => ab.BookId).ToList();

           var allBooks =  appDBContext.Books
          .Include(item => item.Publisher)
          .Include(item => item.Author_Book)
          .ThenInclude(authorbook => authorbook.Author).ToList();
        
            return allBooks.Where(book => authorBooks.Contains(book.Id));   
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
