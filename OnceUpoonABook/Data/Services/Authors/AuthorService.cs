using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using OnceUpoonABook.Data.Base;
using OnceUpoonABook.Data.ViewModels;
using OnceUpoonABook.Models;

namespace OnceUpoonABook.Data.Services.Authors
{
    public class AuthorService : EntityBaseRepository<Author>, IAuthorService
    {
        private readonly AppDBContext appDBContext;

        public AuthorService(AppDBContext appDBContext) : base(appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public Author Add(AddBookNewAuthorViewModel addBookNewAuthor)
        {
            var author = new Author(addBookNewAuthor);
            return Add(author);

        }

        public void DeleteAuthor(int id)
        {
            var authorBooks = appDBContext.Authors_Books.Where(ab => ab.AuthorId == id).ToList();
            foreach (Author_Book ab in authorBooks)
            {
                var bookAuthorsCount = appDBContext.Authors_Books.Where(abDB => abDB.BookId == ab.BookId).Count();
                if (bookAuthorsCount == 1)
                {
                    appDBContext.CartItems.RemoveRange(appDBContext.CartItems.Where(cartItem => cartItem.BookId == ab.BookId));
                    appDBContext.OrderItems.RemoveRange(appDBContext.OrderItems.Where(orderItem => orderItem.BookId == ab.BookId));
                    appDBContext.SaveChanges();
                    var allOrders = appDBContext.Orders.ToList();
                    foreach (var order in allOrders)
                    {
                        //the order doesnt have items
                        if (appDBContext.OrderItems.Any(orderItem => orderItem.OrderId == order.Id) == false)
                        {
                            appDBContext.Orders.Remove(order);
                        }
                    }

                    appDBContext.Books.Remove(appDBContext.Books.First(book => book.Id == ab.BookId));
                }

            }
            appDBContext.Authors_Books.RemoveRange(authorBooks);
            appDBContext.Authors.Remove(appDBContext.Authors.First(author => author.Id == id));
            appDBContext.SaveChanges();
        }
    }
}
