using OnceUpoonABook.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OnceUpoonABook.Data.ViewModels
{
    public class AddBookNewAuthorViewModel
    {

        [Display(Name = "Author Name")]
        [Required(ErrorMessage = "Author Name is required")]
        public string AuthorName { get; set; }

        [Display(Name = "Author Bio")]
        [Required(ErrorMessage = "Author Biography is required")]
        public string AuthorDescription { get; set; }

        [Display(Name = "Author Photo")]
        [Required(ErrorMessage = "Author Photo is required")]
        public string AuthorProfilePicURL { get; set; }

    }
}
