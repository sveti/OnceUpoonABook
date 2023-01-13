using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnceUpoonABook.Data.Base;
using OnceUpoonABook.Data.Enums;
using OnceUpoonABook.Data.ViewModels;

namespace OnceUpoonABook.Models
{
    public class Book: IEntityBase
    {
        public Book()
        {

        }
        public Book(AddBookViewModel addBookViewModel)
        {
            Title = addBookViewModel.Title;
            CoverURL = addBookViewModel.CoverURL;
            PublicationYear = addBookViewModel.PublicationYear;
            Pages = addBookViewModel.Pages;
            Price = addBookViewModel.Price;
            BookCategory = addBookViewModel.BookCategory;
            Language = addBookViewModel.Language;
            PublisherId = addBookViewModel.PublisherId;

        }

        public Book(EditBookViewModel bookViewModel)
        {
            Id = bookViewModel.Book.Id;
            Title = bookViewModel.Book.Title;
            CoverURL = bookViewModel.Book.CoverURL;
            PublicationYear = bookViewModel.Book.PublicationYear;
            Pages = bookViewModel.Book.Pages;
            Price = bookViewModel.Book.Price;
            BookCategory = bookViewModel.Book.BookCategory;
            Language = bookViewModel.Book.Language;
            PublisherId = bookViewModel.Book.PublisherId;
        }

        [Key]
        public int Id { get; set; }

        [Required]
		public string Title { get; set; }

        public string CoverURL { get; set; }

        [Required]
        [Display(Name = "Year Published")]
        public int PublicationYear { get; set; }
        [Required]
        public int Pages { get; set; }

        public double Price { get; set; }

        [Display(Name ="Book Category")]
		public BookCategory BookCategory { get; set; }

        public Language Language { get; set; }

        //Relationships
        public List<Author_Book> Author_Book { get; set; }

		public int PublisherId { get; set; }
		[ForeignKey("PublisherId")]
		public Publisher Publisher { get; set; }

    }
}
