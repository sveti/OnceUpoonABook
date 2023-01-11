using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using OnceUpoonABook.Data.Base;
using OnceUpoonABook.Data.ViewModels;
using OnceUpoonABook.Models;

namespace OnceUpoonABook.Data.Services
{
    public class AuthorService : EntityBaseRepository<Author>, IAuthorService
    {
        private readonly AppDBContext appDBContext;

        public AuthorService(AppDBContext appDBContext): base(appDBContext) { 
        
        }

        public Author Add(AddBookNewAuthorViewModel addBookNewAuthor)
        {
            var author = new Author(addBookNewAuthor);
            return Add(author);    

        }
    }
}
