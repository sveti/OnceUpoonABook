using OnceUpoonABook.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OnceUpoonABook.Models
{
	public class Author: IEntityBase
	{
        [Key]
        public int Id { get; set; }

        [Display(Name ="Author Name")]
        [Required(ErrorMessage ="Author Name is required")]
        public string AuthorName { get; set; }

        [Display(Name = "Author Bio")]
        [Required(ErrorMessage = "Author Biography is required")]
        public string AuthorDescription { get; set; }

        [Display(Name = "Author Photo")]
        [Required(ErrorMessage = "Author Photo is required")]
        public string AuthorProfilePicURL { get; set; }

        [AllowNull]
        public List<Author_Book> Author_Book { get; set; }


    }
}
