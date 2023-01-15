using OnceUpoonABook.Models;

namespace OnceUpoonABook.Data.Services.Orders
{
    public interface IOrdersService
    {
        void Add(List<CartItem> items, string userId);
        List<Order> GetOrdersByUserId(string userId);

        public double GetTotal(string userId, int orderId);
    }
}
