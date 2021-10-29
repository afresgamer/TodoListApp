using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.Repository;
using ToDoApp_backend.ViewModel;

namespace ToDoApp_backend.UseCase
{
    public class BookUseCase
    {
        private readonly IBookRepository _repository;

        public BookUseCase(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BookViewModel>> Get()
        {
            var result = await _repository.FetchBookAsync();

            return result;
        }
    }
}
