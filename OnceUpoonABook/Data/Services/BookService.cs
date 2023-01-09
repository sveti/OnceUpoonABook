using Microsoft.EntityFrameworkCore;
using OnceUpoonABook.Data.Base;
using OnceUpoonABook.Models;

namespace OnceUpoonABook.Data.Services
{
    public class BookService : EntityBaseRepository<Book>, IBookService
    {
        private readonly AppDBContext appDBContext;

        public BookService(AppDBContext appDBContext) : base(appDBContext) { }

    }
   
}
