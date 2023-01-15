using OnceUpoonABook.Data.Services.Orders;
using OnceUpoonABook.Models;

namespace OnceUpoonABook.Data.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<CartItem> CartItems{ get; set; }
        public double Total { get; set; }   
    }
}
