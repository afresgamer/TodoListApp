using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.ViewModel.Signup;
using ToDoApp_backend.Context;
using ToDoApp_backend.Repository.Base;

namespace ToDoApp_backend.Repository.User
{
    public interface IUserRepository
    {
        Task<DB.User> InsertSignupUser(SignupViewModel viewModel, DB.License license);
    }

    public class UserRepository : RepositoryBase<DB.User>, IUserRepository
    {
        public UserRepository(MainContext db) : base(db)
        {

        }

        public async Task<DB.User> InsertSignupUser(SignupViewModel viewModel, DB.License license)
        {
            if (viewModel == null || license == null) return null;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var user = new DB.User
                    {
                        Name = viewModel.UserName,
                        NameKana = viewModel.UserNameKana,
                        CreateDate = DateTime.Now,
                        LicenseId = license.Id
                    };

                    await Add(user);
                    await db.SaveChangesAsync();

                    transaction.Commit();

                    var data = await db.Users
                        .AsNoTracking()
                        .Where(x => x.Name == user.Name && x.NameKana == user.NameKana)
                        .FirstOrDefaultAsync();
                    return data;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
