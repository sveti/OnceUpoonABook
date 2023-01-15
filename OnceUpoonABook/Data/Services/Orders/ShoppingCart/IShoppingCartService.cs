using OnceUpoonABook.Migrations;
using OnceUpoonABook.Models;
using System;

namespace OnceUpoonABook.Data.Services.Orders.ShoppingCart
{
    public interface IShoppingCartService
    {
        public void AddItemToCart(Book book, string userId);

        public void RemoveItemFromCart(Book book, string userId);

        public List<CartItem> GetItems(string userId);

        public double GetTotal(string userId);

        public void Clear(string userId);
    }
}
