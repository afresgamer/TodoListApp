using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.Context;
using ToDoApp_backend.Repository.Base;
using ToDoApp_backend.ViewModel.Setting;

namespace ToDoApp_backend.Repository.Setting
{
    public interface ISettingRepository
    {
        Task<DB.Setting> FindSetting(long userId);
        Task<SettingViewModel> FindSettingViewModel(long userId);
        Task<bool> UpsertSetting(SettingViewModel viewModel, long userId);
    }

    public class SettingRepository : RepositoryBase<DB.Setting>, ISettingRepository
    {
        private readonly IMapper _mapper;

        public SettingRepository(MainContext db, IMapper mapper) :base(db)
        {
            _mapper = mapper;
        }

        public async Task<DB.Setting> FindSetting(long userId)
        {
            var result = await db.Settings
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<SettingViewModel> FindSettingViewModel(long userId)
        {
            var data = await db.Settings
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            var result = data != null ? _mapper.Map<SettingViewModel>(data) : new SettingViewModel();

            return result;
        }

       public async Task<bool> UpsertSetting(SettingViewModel viewModel, long userId)
        {
            if (viewModel == null) return false;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var data = _mapper.Map<DB.Setting>(viewModel);
                    data.UserId = userId;
                    data.CreateDate = DateTime.Now;

                    var result = await db.Settings
                        .AsNoTracking()
                        .Where(x => x.UserId == userId)
                        .FirstOrDefaultAsync();

                    if (result == null) await Add(data);
                    else
                    {
                        result.NotificationSettingsFlg = data.NotificationSettingsFlg;
                        result.CategoryMasterFlg = data.CategoryMasterFlg;
                        result.UpdateDate = DateTime.Now;
                        Update(result);
                    }
                    await db.SaveChangesAsync();
                    transaction.Commit();

                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
