using OnceUpoonABook.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnceUpoonABook.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; }
        public int Amount { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public OnceUpoonABookUser User { get; set; }

    }
}
