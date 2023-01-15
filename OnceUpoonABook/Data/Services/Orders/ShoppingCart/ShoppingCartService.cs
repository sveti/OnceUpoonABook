using Microsoft.EntityFrameworkCore;
using OnceUpoonABook.Models;
using System;

namespace OnceUpoonABook.Data.Services.Orders.ShoppingCart
{

    public class ShoppingCartService : IShoppingCartService
    {
        public AppDBContext appDBContext { get; set; }

        public ShoppingCartService(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public void AddItemToCart(Book book, string userId)
        {
            var item = appDBContext
                .CartItems.FirstOrDefault(n => n.Book.Id == book.Id && n.UserId == userId);

            if (item == null)
            {
                item = new CartItem()
                {
                    UserId = userId,
                    Book = book,
                    Amount = 1
                };

                appDBContext.CartItems.Add(item);
            }
            else
            {
                item.Amount++;
            }
            appDBContext.SaveChanges();
        }

        public void RemoveItemFromCart(Book book, string userId)
        {
            var shoppingCartItem = appDBContext
                .CartItems.FirstOrDefault(item => item.Book.Id == book.Id && item.UserId == userId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    appDBContext.CartItems.Remove(shoppingCartItem);
                }
            }
            appDBContext.SaveChanges();
        }

        public List<CartItem> GetItems(string userId)
        {

            return appDBContext.CartItems.Where(item => item.UserId == userId)
                    .Include(item => item.Book)
                    .ThenInclude(book => book.Publisher)
                    .Include(book => book.Book.Author_Book)
                    .ThenInclude(ab => ab.Author).ToList(); ;

        }

        public double GetTotal(string userId)
        {
            double sum = 0;
            foreach (var item in GetItems(userId))
            {
                sum +=  (item.Book.Price * item.Amount);
            }
            return Math.Truncate(sum*100)/100;

        }

        public void Clear(string userId)
        {
            appDBContext.CartItems.RemoveRange(appDBContext.CartItems.Where(n => n.UserId == userId));
            appDBContext.SaveChanges();
        }
    }
}
