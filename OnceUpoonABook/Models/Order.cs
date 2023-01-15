using OnceUpoonABook.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnceUpoonABook.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public OnceUpoonABookUser User { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
