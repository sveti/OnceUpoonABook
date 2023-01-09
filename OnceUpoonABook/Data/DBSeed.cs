using Microsoft.AspNetCore.Builder;
using OnceUpoonABook.Models;

namespace OnceUpoonABook.Data
{
	public class DBSeed
	{

		public static void Seed(IApplicationBuilder applicationBuilder)
		{

			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetService<AppDBContext>();

				context.Database.EnsureCreated();

				if (!context.Authors.Any())
				{
					context.Authors.AddRange(new List<Author>()
					{
						new Author()
						{
							AuthorName = "Terry Pratchett",
							AuthorDescription = "Sir Terence David John Pratchett OBE was an English humourist, satirist, and author of fantasy novels, especially comical works. He is best known for his Discworld series of 41 novels. Pratchett's first novel, The Carpet People, was published in 1971.",
							AuthorProfilePicURL = "https://external-preview.redd.it/joVllrJbdzimVMRCXgocdvs6_nZEst5Urf6y5d27tjw.jpg?auto=webp&s=f7ea424e8b2e4277fa54333f3bd37ec4aef5f3ef"

						},
						new Author()
						{
							AuthorName= "Neil Gaiman",
							AuthorDescription="Neil Richard MacKinnon Gaiman; is an English author of short fiction, novels, comic books, graphic novels, nonfiction, audio theatre, and films. His works include the comic book series The Sandman and novels Stardust, American Gods, Coraline, and The Graveyard Book.",
							AuthorProfilePicURL="http://t3.gstatic.com/licensed-image?q=tbn:ANd9GcT8gIqv52qvWIZKjbvpneP_4MWQ-qrYuUXp-XeFL9mA-ft1DMFbU2bsQEgv-4I1mjA3veFxzeUmUU-WW8s"
						},
						new Author()
						{
							AuthorName = "J. R. R. Tolkien",
							AuthorDescription="John Ronald Reuel Tolkien CBE FRSL was an English writer and philologist. He was the author of the high fantasy works The Hobbit and The Lord of the Rings. From 1925 to 1945, Tolkien was the Rawlinson and Bosworth Professor of Anglo-Saxon and a Fellow of Pembroke College, both at the University of Oxford.",
							AuthorProfilePicURL="https://www.biography.com/.image/t_share/MTE5NTU2MzE2Mzg4MzYxNzM5/jrr-tolkien-9508428-1-402.jpg"
						}
					});
					context.SaveChanges();
				}

				if (!context.Publishers.Any())
				{
					context.Publishers.AddRange(new List<Publisher>(){
						new Publisher()
						{
							Name = "Artline Studios",
							YearCreated = 2005,
							LogoURL = "https://artline-store.net/userfiles/logo/WHITE-Artline_Studios_logo_square.png"
						},
						new Publisher(){
							Name = "Ciela Books",
							YearCreated = 2006,
							LogoURL = "https://www.ciela.com/media/logo/websites/1/Logo-Ciela_1.png"

						}
					});
					context.SaveChanges();
				}

				if (!context.Books.Any())
				{
					context.Books.AddRange(new List<Book>()
					{
						new Book()
						{
							Title = "The Colour of Magic",
							PublicationYear = 1983,
							Pages = 240,
							BookCategory = Enums.BookCategory.Fantasy,
							Language = Enums.Language.English,
							PublisherId = 1,
							CoverURL="https://static.wikia.nocookie.net/discworld/images/7/76/TCoM_cover.jpg/revision/latest?cb=20070107074953",
							Price = 8.49

                        },
						new Book()
						{
							Title = "The ​Graveyard Book",
							PublicationYear = 2008,
							Pages = 320,
							BookCategory = Enums.BookCategory.Fantasy,
							Language = Enums.Language.English,
							PublisherId = 1,
							CoverURL="https://m.media-amazon.com/images/I/51f63bXc2NL.jpg",
                            Price = 8.99

                        },
						  new Book()
						{
							Title = "Good Omens",
							PublicationYear = 1990,
							Pages = 288,
							BookCategory = Enums.BookCategory.Fantasy,
							Language = Enums.Language.English,
							PublisherId = 2,
							CoverURL="https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/g/o/571d3020ca7d9df9806ca66f0a390d24/good-omens-30.jpg",
							Price = 9.49

                        }

					});
					context.SaveChanges();
				}

				if (!context.Authors_Books.Any())
				{

					context.Authors_Books.AddRange(new List<Author_Book>() { 
						
						new Author_Book()
						{
							BookId = 1,
							AuthorId = 1
						}
						,
                        new Author_Book()
                        {
                            BookId = 2,
                            AuthorId = 2
                        }
						,
                        new Author_Book()
                        {
                            BookId = 3,
                            AuthorId = 1
                        }
						,
                        new Author_Book()
                        {
                            BookId = 3,
                            AuthorId = 2
                        }



                    });
					context.SaveChanges();

				}
			}
		}
	}
}
