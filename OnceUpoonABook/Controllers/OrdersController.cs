using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnceUpoonABook.Data.Services.Books;
using OnceUpoonABook.Data.Services.Orders;
using OnceUpoonABook.Data.Services.Orders.ShoppingCart;
using OnceUpoonABook.Data.ViewModels;
using OnceUpoonABook.Models;
using System.Security.Claims;

namespace OnceUpoonABook.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IBookService bookService;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IOrdersService orderService;

        public OrdersController(IBookService bookService, IShoppingCartService shoppingCartService, IOrdersService orderService)
        {
            this.bookService = bookService;
            this.shoppingCartService = shoppingCartService;
            this.orderService = orderService;
        }


        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = orderService.GetOrdersByUserId(userId);

            ViewBag.UserId = userId;

            return View(orders);
        }
        public IActionResult ShoppingCart()
        {

            var shoppingCartVM = new ShoppingCartViewModel();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            shoppingCartVM.CartItems = shoppingCartService.GetItems(userId);
            shoppingCartVM.Total = shoppingCartService.GetTotal(userId);


            return View(shoppingCartVM);
        }

        public RedirectToActionResult AddToShoppingCart(int id)
        {

            var book = bookService.GetBookById(id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (book != null)
            {
                shoppingCartService.AddItemToCart(book, userId);   
            }

            return RedirectToAction(nameof(ShoppingCart));
        }

        public RedirectToActionResult RemoveFromShoppingCart(int id)
        {

            var book = bookService.GetBookById(id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (book != null)
            {
                shoppingCartService.RemoveItemFromCart(book, userId);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }

        public IActionResult Checkout()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            orderService.Add(shoppingCartService.GetItems(userId), userId);
            shoppingCartService.Clear(userId);
            return View("Checkout");
        }
    }
}
