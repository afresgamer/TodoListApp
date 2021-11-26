﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.Context;
using ToDoApp_backend.Repository.Base;
using ToDoApp_backend.Repository.Setting;
using ToDoApp_backend.ViewModel.Schedule;

namespace ToDoApp_backend.Repository.Schedule
{
    public interface IScheduleRepository
    {
        Task<ScheduleViewModel> GetSchedule(long scheduleId, long userId);
        Task<long> InsertSchedule(ScheduleViewModel viewModel, long userId);
        Task<bool> UpdateSchedule(ScheduleViewModel viewModel, long userId);
        Task DeleteSchedule(long scheduleId);
    }

    public class ScheduleRepository : RepositoryBase<DB.Schedule>, IScheduleRepository
    {
        private readonly IMapper _mapper;
        private readonly ISettingRepository _settingRepository;

        public ScheduleRepository(MainContext db, IMapper mapper, ISettingRepository settingRepository) : base(db)
        {
            _mapper = mapper;
            _settingRepository = settingRepository;
        }

        public async Task<ScheduleViewModel> GetSchedule(long scheduleId, long userId)
        {
            var data = await FindAsync(scheduleId);

            var result = _mapper.Map<ScheduleViewModel>(data) ?? new ScheduleViewModel();
            var setting = await _settingRepository.FindSetting(userId);

            result.NoticeSettingFlg = setting.NotificationSettingsFlg;
            result.CategoryMasterFlg = setting.CategoryMasterFlg;

            return result;
        }

        public async Task<long> InsertSchedule(ScheduleViewModel viewModel, long userId)
        {
            if (viewModel == null) return -1;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var data = _mapper.Map<DB.Schedule>(viewModel);
                    data.UserId = userId;
                    data.CreateDate = DateTime.Now;

                    await Add(data);
                    await db.SaveChangesAsync();

                    transaction.Commit();

                    var result = await db.Schedules
                        .AsNoTracking()
                        .Where(x => x.UserId == userId && x.Name == viewModel.ScheduleName
                            && x.StartDay == viewModel.StartDay && x.EndDay == viewModel.EndDay
                            && x.UsageTime == viewModel.UsageTime)
                        .FirstOrDefaultAsync();
                    return result.Id;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<bool> UpdateSchedule(ScheduleViewModel viewModel, long userId)
        {
            if (viewModel == null) return false;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var data = await db.Schedules
                        .AsNoTracking()
                        .Where(x => x.UserId == userId && x.Name == viewModel.ScheduleName)
                        .FirstOrDefaultAsync();
                    data.Summary = viewModel.Summary;
                    data.StartDay = viewModel.StartDay;
                    data.EndDay = viewModel.EndDay;
                    data.UsageTime = viewModel.UsageTime;
                    data.SlackFlg = data.SlackFlg;
                    data.LineFlg = viewModel.LineFlg;
                    data.CategoryMasterId = viewModel.CategoryMasterId;
                    data.UpdateDate = DateTime.Now;

                    Update(data);
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

        public async Task DeleteSchedule(long scheduleId)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    await Remove(scheduleId);
                    await db.SaveChangesAsync();

                    transaction.Commit();
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