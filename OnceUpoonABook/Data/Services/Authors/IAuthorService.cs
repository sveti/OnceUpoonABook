﻿using OnceUpoonABook.Data.Base;
using OnceUpoonABook.Data.ViewModels;
using OnceUpoonABook.Models;

namespace OnceUpoonABook.Data.Services.Authors
{
    public interface IAuthorService : IEntityBaseRepository<Author>
    {
        Author Add(AddBookNewAuthorViewModel addBookNewAuthor);
        void DeleteAuthor(int id);
    }
}
