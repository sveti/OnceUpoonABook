using Microsoft.EntityFrameworkCore;
using OnceUpoonABook.Data.Base;
using OnceUpoonABook.Models;

namespace OnceUpoonABook.Data.Services
{
    public class AuthorService : EntityBaseRepository<Author>, IAuthorService
    {
        private readonly AppDBContext appDBContext;

        public AuthorService(AppDBContext appDBContext): base(appDBContext) { }


    }
}
