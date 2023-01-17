using OnceUpoonABook.Data.Base;
using OnceUpoonABook.Models;

namespace OnceUpoonABook.Data.Services.Publishers
{
    public interface IPublisherService : IEntityBaseRepository<Publisher>
    {
        void DeletePublisher(int id);
    }
}
