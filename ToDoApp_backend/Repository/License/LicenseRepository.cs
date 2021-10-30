using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Utility;
using ToDoApp_backend.Context;
using ToDoApp_backend.Repository.Base;

namespace ToDoApp_backend.Repository.License
{
    public interface ILicenseRepository
    {
        Task<DB.License> InsertInitLicense(string password);
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
    }
}
