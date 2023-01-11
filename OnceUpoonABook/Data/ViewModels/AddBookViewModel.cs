using OnceUpoonABook.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OnceUpoonABook.Data.ViewModels
{
    public class AddBookViewModel {


        [Required(ErrorMessage ="Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Book cover is required")]
        [Display(Description ="Book Cover URL")]
        public string CoverURL { get; set; }

        [Required(ErrorMessage = "Publication Year is required")]
        [Display(Description = "Publication Year")]
        public int PublicationYear { get; set; }

        [Required(ErrorMessage = "Number of Pages is required")]
        public int Pages { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        public BookCategory BookCategory { get; set; }

        public Language Language { get; set; }

        [Required(ErrorMessage = "Select publisher")]
        public int PublisherId { get; set; }

        [Required(ErrorMessage = "Select an author")]
        [Display(Description = "Author(s):")]
        public List<int> AuthorIds { get; set; } = new();

        //? allows null
        public AddBookNewAuthorViewModel? newAuthor { get; set; }


    }
}
