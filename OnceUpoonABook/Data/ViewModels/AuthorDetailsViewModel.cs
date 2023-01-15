using OnceUpoonABook.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OnceUpoonABook.Data.ViewModels
{
    public class AuthorDetailsViewModel
    {
        public AuthorDetailsViewModel()
        {
                
        }
        public AuthorDetailsViewModel(Author authorDetails)
        {
            Id = authorDetails.Id;
            AuthorName= authorDetails.AuthorName;
            AuthorDescription = authorDetails.AuthorDescription;
            AuthorProfilePicURL = authorDetails.AuthorProfilePicURL;
            Books = new List<Book>();
        }

        public int Id { get; set; }

        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }

        [Display(Name = "Author Bio")]
        public string AuthorDescription { get; set; }

        [Display(Name = "Author Photo")]
        public string AuthorProfilePicURL { get; set; }

        public List<Book> Books { get; set; } 

    }
}
