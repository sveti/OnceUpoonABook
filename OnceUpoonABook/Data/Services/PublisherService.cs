using OnceUpoonABook.Data.Base;
using OnceUpoonABook.Models;

namespace OnceUpoonABook.Data.Services
{
    public class PublisherService : EntityBaseRepository<Publisher>, IPublisherService
    {
        private readonly AppDBContext appDBContext;

        public PublisherService(AppDBContext appDBContext) : base(appDBContext) { }


    }

}
