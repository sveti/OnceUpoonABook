using Microsoft.EntityFrameworkCore;
using OnceUpoonABook.Models;

namespace OnceUpoonABook.Data
{
	public class AppDBContext: DbContext
	{
		public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			///set the compound key as combination of author id and book id
			modelBuilder.Entity<Author_Book>().HasKey(AuthorBook => new
			{
				AuthorBook.AuthorId,
				AuthorBook.BookId
			});

			//define the M to M
			modelBuilder.Entity<Author_Book>().HasOne(book => book.Book).WithMany(authorBook => authorBook.Author_Book).HasForeignKey(book => book.BookId);
			modelBuilder.Entity<Author_Book>().HasOne(author => author.Author).WithMany(authorBook => authorBook.Author_Book).HasForeignKey(author => author.AuthorId);

            base.OnModelCreating(modelBuilder);

        }

		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<Publisher> Publishers { get; set; }
		public DbSet<Author_Book> Authors_Books { get; set; }

    }
}
