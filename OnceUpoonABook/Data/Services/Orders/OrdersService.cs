using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;
using OnceUpoonABook.Models;
using System;

namespace OnceUpoonABook.Data.Services.Orders
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDBContext appDBContext;
        public OrdersService(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public void Add(List<CartItem> items, string userId)
        {
            var order = new Order { UserId = userId};
            appDBContext.Add(order);
            appDBContext.SaveChanges();

            foreach (var item in items)
            {
                var orderItem = new OrderItem(item);
                orderItem.OrderId = order.Id;
                appDBContext.Add(orderItem);
            }
            appDBContext.SaveChanges();

        }

        public List<Order> GetOrdersByUserId(string userId)
        {
            return appDBContext.Orders
                .Include(order => order.OrderItems)
                .ThenInclude(orderItem => orderItem.Book)
                .ThenInclude(book => book.Author_Book)
                .ThenInclude(authorHasBook => authorHasBook.Author)
                .Include(order => order.OrderItems).ThenInclude(orderItem => orderItem.Book.Publisher)
                .Where(order => order.UserId == userId) 
                .ToList();
        }

        public double GetTotal(string userId, int orderId)
        {
            double sum = 0;
            foreach (var item in GetOrdersByUserId(userId).First(order => order.Id == orderId).OrderItems)
            {
                sum += (item.Book.Price * item.Amount);
            }
            return Math.Truncate(sum * 100) / 100;
        }
    }
}
