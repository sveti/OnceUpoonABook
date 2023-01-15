using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnceUpoonABook.Models
{
    public class OrderItem
    {
        public OrderItem()
        {

        }
        public OrderItem(CartItem item)
        {
            Amount = item.Amount;
            Price = item.Book.Price;
            BookId= item.Book.Id;
        }

        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }
        public double Price { get; set; }

        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
