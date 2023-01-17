using OnceUpoonABook.Data.Base;
using OnceUpoonABook.Models;
using System;
using System.Linq;

namespace OnceUpoonABook.Data.Services.Publishers
{
    public class PublisherService : EntityBaseRepository<Publisher>, IPublisherService
    {
        private readonly AppDBContext appDBContext;

        public PublisherService(AppDBContext appDBContext) : base(appDBContext) { 
        
            this.appDBContext = appDBContext;   
        }

        public void DeletePublisher(int id)
        {
            var booksToBeDeleted = appDBContext.Books.Where(book => book.PublisherId == id).ToList();

            foreach (var book in booksToBeDeleted)
            {
                appDBContext.CartItems.RemoveRange(appDBContext.CartItems.Where(cartItem => cartItem.BookId == book.Id));
                appDBContext.OrderItems.RemoveRange(appDBContext.OrderItems.Where(orderItem => orderItem.BookId == book.Id));
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
            }
            
            appDBContext.Books.RemoveRange(booksToBeDeleted);

            appDBContext.Publishers.Remove(appDBContext.Publishers.First(x => x.Id == id));

            appDBContext.SaveChanges();
        }
    }

}
