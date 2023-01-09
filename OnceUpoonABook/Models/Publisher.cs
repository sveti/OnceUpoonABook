using OnceUpoonABook.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OnceUpoonABook.Models
{
	public class Publisher: IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Name of the publihser is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Year of creation is required")]
        public int YearCreated { get; set; }

		[Display(Name = "Logo")]
        [Required(ErrorMessage = "Link to the logo of the publihser is required")]
        public string LogoURL { get; set; }

        [AllowNull]
		public List<Book> Books { get; set; }
		
	}
}
