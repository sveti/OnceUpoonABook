using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OnceUpoonABook.Areas.Identity.Data;
using OnceUpoonABook.Models;

namespace OnceUpoonABook.Data
{
    public class DBSeed
    {

        public static async Task Seed(IApplicationBuilder applicationBuilder, ConfigurationManager configuration)
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
                            Price = 8.49,
                            Description="In a world supported on the back of a giant turtle (sex unknown), a gleeful, explosive, wickedly eccentric expedition sets out. There's an avaricious but inept wizard, a naive tourist whose luggage moves on hundreds of dear little legs, dragons who only exist if you believe in them, and of course THE EDGE of the planet..."

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
                            Price = 8.99,
                            Description="Nobody Owens, known to his friends as Bod, is a perfectly normal boy. Well, he would be perfectly normal if he didn't live in a graveyard, being raised and educated by ghosts, with a solitary guardian who belongs to neither the world of the living nor the world of the dead."

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
                            Price = 9.49,
                            Description="People have been predicting the end of the world almost from its very beginning, so it’s only natural to be sceptical when a new date is set for Judgement Day. This time though, the armies of Good and Evil really do appear to be massing. The four Bikers of the Apocalypse are hitting the road. But both the angels and demons – well, one fast-living demon and a somewhat fussy angel – would quite like the Rapture not to happen."

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

                if (!context.Roles.Any())
                {
                    var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                    string[] roleNames = { "Admin", "Member" };
                    IdentityResult roleResult;

                    foreach (var roleName in roleNames)
                    {
                        var roleExist = await roleManager.RoleExistsAsync(roleName);
                        if (!roleExist)
                        {
                            //create the roles and seed them to the database
                            roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                        }
                    }

                }

                if (!context.Users.Any())
                {
                    //https://stackoverflow.com/questions/42471866/how-to-create-roles-in-asp-net-core-and-assign-them-to-users

                    var userManager = serviceScope.ServiceProvider.GetService<UserManager<OnceUpoonABookUser>>();

                    //Here you could create a super user who will maintain the web app
                    var user = new OnceUpoonABookUser
                    {

                        UserName = configuration["AppSettings:UserName"],
                        Email = configuration["AppSettings:UserEmail"],
                    };
                    //Ensure you have these values in your appsettings.json file
                    string userPassword = configuration["AppSettings:UserPassword"];


                    var admin = await userManager.CreateAsync(user, userPassword);
                    if (admin.Succeeded)
                    {
                        //here we tie the new user to the role
                        await userManager.AddToRoleAsync(user, "Admin");

                    }


                }

            }
        }
    }
}
