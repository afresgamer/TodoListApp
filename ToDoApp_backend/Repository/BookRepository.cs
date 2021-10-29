using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.Context;
using ToDoApp_backend.DB;
using ToDoApp_backend.Repository.Base;
using ToDoApp_backend.ViewModel;

namespace ToDoApp_backend.Repository
{
    public interface IBookRepository
    {
        Task<List<BookViewModel>> FetchBookAsync();
    }

    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        private readonly IMapper _mapper;
        public BookRepository(MainContext db, IMapper mapper) : base(db)
        {
            _mapper = mapper;
        }

        public async Task<List<BookViewModel>> FetchBookAsync()
        {
            var data = await db.Books
                .AsNoTracking()
                .Where(x => x.Id > 0)
                .ToListAsync();

            var result = _mapper.Map<List<BookViewModel>>(data);

            return result;
        }
    }
}
