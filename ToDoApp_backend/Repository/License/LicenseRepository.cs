using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.Utility;
using ToDoApp_backend.Context;
using ToDoApp_backend.Repository.Base;

namespace ToDoApp_backend.Repository.License
{
    public interface ILicenseRepository
    {
        Task<DB.License> InsertInitLicense(string password);
        Task<bool> UpdateLicenseAsync(DB.User user, bool isUseFlg);
        Task<bool> UpdateResetPasswordAsync(DB.User user, string password);
    }

    public class LicenseRepository : RepositoryBase<DB.License>, ILicenseRepository
    {
        public LicenseRepository(MainContext db) : base(db)
        {

        }

        public async Task<DB.License> InsertInitLicense(string password)
        {
            if (string.IsNullOrEmpty(password)) return null;

            var (hash, salt) = PasswordUtility.HashPassword(password);

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var license = new DB.License
                    {
                        UseFlg = false,
                        Hash = hash,
                        Salt = salt,
                        CreateDate = DateTime.Now
                    };

                    await Add(license);
                    await db.SaveChangesAsync();

                    transaction.Commit();

                    var data = await db.Licenses
                        .AsNoTracking()
                        .Where(x => x.Salt == license.Salt && x.Hash == license.Hash)
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

        public async Task<bool> UpdateLicenseAsync(DB.User user, bool isUseFlg)
        {
            if (user == null) return false;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var license = await FindAsync(user.LicenseId);

                    license.UseFlg = isUseFlg;
                    license.UpdateDate = DateTime.Now;
                    Update(license);
                    await db.SaveChangesAsync();

                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<bool> UpdateResetPasswordAsync(DB.User user, string password)
        {
            if (user == null) return false;

            var (hash, salt) = PasswordUtility.HashPassword(password);

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var license = await FindAsync(user.LicenseId);

                    license.Hash = hash;
                    license.Salt = salt;
                    license.UpdateDate = DateTime.Now;
                    Update(license);
                    await db.SaveChangesAsync();

                    transaction.Commit();
                    return true;
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
