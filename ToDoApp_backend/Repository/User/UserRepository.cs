using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.Utility;
using ToDoApp_backend.ViewModel.Login;
using ToDoApp_backend.ViewModel.Signup;
using ToDoApp_backend.Auth;
using ToDoApp_backend.Context;
using ToDoApp_backend.Repository.Base;

namespace ToDoApp_backend.Repository.User
{
    public interface IUserRepository
    {
        Task<DB.User> FindUserInfo(long userId);
        Task<DB.User> FindLoginUser(LoginViewModel loginViewModel);
        Task<bool> FindLoginInfo(UserSession userSession);
        Task<DB.User> InsertSignupUser(SignupViewModel viewModel, DB.License license);
    }

    public class UserRepository : RepositoryBase<DB.User>, IUserRepository
    {
        public UserRepository(MainContext db) : base(db)
        {

        }

        public async Task<DB.User> FindUserInfo(long userId)
        {
            if (userId <= 0) return null;
            var user = await FindAsync(userId);

            return user ?? null;
        }

        public async Task<bool> FindLoginInfo(UserSession userSession)
        {
            if (userSession.UserId <= 0) return false;

            var user = await FindAsync(userSession.UserId);

            var result = await db.Licenses
                .AsNoTracking()
                .Where(x => x.Id == user.LicenseId)
                .FirstOrDefaultAsync();

            return result != null;
        }

        public async Task<DB.User> FindLoginUser(LoginViewModel loginViewModel)
        {
            if (loginViewModel == null) return null;

            var user = await FindAsync(loginViewModel.UserId);

            var license = await db.Licenses
                .AsNoTracking()
                .Where(x => x.Id == user.LicenseId)
                .FirstOrDefaultAsync();

            var result = PasswordUtility.VerifyPassword(license.Hash, loginViewModel.Password, license.Salt);

            return result ? user : null;
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
