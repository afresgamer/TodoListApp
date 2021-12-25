using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.Context;
using ToDoApp_backend.Repository.Base;
using ToDoApp_backend.Repository.CategoryMaster;
using ToDoApp_backend.Repository.Schedule;
using ToDoApp_backend.Repository.Setting;
using ToDoApp_backend.ViewModel.Calendar;

namespace ToDoApp_backend.Repository.Calendar
{
    public interface ICalendarRepository
    {
        Task<CalendarEventViewModel> FindCalendarAsync(long calendarId,long userId);
        Task<List<CalendarEventViewModel>> FetchCalendarListAsync(long userId);
        Task<bool> InsertCalendar(CalendarEventViewModel viewModel, long userId);
        Task<bool> InsertCalendarFromSchedule(long scheduleId, long userId);
        Task<bool> UpdateCalendar(CalendarEventViewModel viewModel, long userId);
        Task DeleteCalendar(long scheduleId);
    }

    public class CalendarRepository : RepositoryBase<DB.Calendar>, ICalendarRepository
    {
        private readonly IMapper _mapper;
        private readonly ICategoryMasterRepository _categoryMasterReposioty;
        private readonly ISettingRepository _settingRepository;
        private readonly IScheduleRepository _scheduleRepository;

        public CalendarRepository(
            MainContext db,
            IMapper mapper,
            ICategoryMasterRepository categoryMasterRepository,
            ISettingRepository settingRepository,
            IScheduleRepository scheduleRepository) : base(db)
        {
            _mapper = mapper;
            _categoryMasterReposioty = categoryMasterRepository;
            _settingRepository = settingRepository;
            _scheduleRepository = scheduleRepository;
        }

        public async Task<CalendarEventViewModel> FindCalendarAsync(long calendarId, long userId)
        {
            if (calendarId == 0)
            {
                var initSetting = await _settingRepository.FindSetting(userId);

                var initResult = new CalendarEventViewModel
                {
                    Start = DateTime.Now,
                    End = DateTime.Now,
                    ExtendedProps = new CalendarExtendedProps()
                };
                initResult.ExtendedProps.CategoryMasterFlg = initSetting.CategoryMasterFlg;
                return initResult;
            }

            var data = await db.Calendars
                .AsNoTracking()
                .Where(x => x.UserId == userId && x.Id == calendarId)
                .FirstOrDefaultAsync();
            var schedule = await db.Schedules
                .AsNoTracking()
                .Where(x => x.UserId == userId && x.Id == data.ScheduleId)
                .FirstOrDefaultAsync();
            var categoryMaster = await _categoryMasterReposioty.FindCategoryMaster(schedule.CategoryMasterId);
            var setting = await _settingRepository.FindSetting(userId);

            var result = _mapper.Map<CalendarEventViewModel>(data);
            result.ExtendedProps = new CalendarExtendedProps
            {
                ScheduleId = data.ScheduleId,
                Summary = schedule.Summary,
                UsageTime = schedule.UsageTime,
                CategoryMasterFlg = setting.CategoryMasterFlg,
                CategoryMasterId = categoryMaster.Id
            };

            return result;
        }

        public async Task<List<CalendarEventViewModel>> FetchCalendarListAsync(long userId)
        {
            var data = await db.Calendars
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToListAsync();

            var result = _mapper.Map<List<CalendarEventViewModel>>(data);

            foreach(var eventObj in result)
            {
                eventObj.ExtendedProps = new CalendarExtendedProps
                {
                    ScheduleId = data
                    .Where(x => x.Id == eventObj.Id && x.Title == eventObj.Title)
                    .Select(x => x.ScheduleId)
                    .FirstOrDefault()
                };
                var schedule = await db.Schedules
                    .AsNoTracking()
                    .Where(x => x.Id == eventObj.ExtendedProps.ScheduleId && x.UserId == userId)
                    .FirstOrDefaultAsync();
                var categoryMaster = await _categoryMasterReposioty.FindCategoryMaster(schedule.CategoryMasterId);
                var setting = await _settingRepository.FindSetting(userId);

                eventObj.ExtendedProps.Summary = schedule.Summary;
                eventObj.ExtendedProps.UsageTime = schedule.UsageTime;
                eventObj.ExtendedProps.CategoryMasterId = categoryMaster.Id;
                eventObj.ExtendedProps.CategoryMasterFlg = setting.CategoryMasterFlg;
                //　TODO カテゴリーマスタから文字色と文字のボーダー色を設定出来るようにする
                //eventObj.BackgroundColor = "#0000FF";
                //eventObj.BorderColor = "#000000";
            }

            return result;
        }

        public async Task<bool> InsertCalendar(CalendarEventViewModel viewModel, long userId)
        {
            if (viewModel == null) return false;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var data = _mapper.Map<DB.Calendar>(viewModel);
                    data.UserId = userId;
                    data.CreateDate = DateTime.Now;
                    
                    DB.Schedule schedule = new DB.Schedule
                    {
                        UserId = userId,
                        CreateDate = DateTime.Now,
                        Name = data.Title,
                        StartDay = (DateTime)data.Start,
                        EndDay = (DateTime)data.End,
                        Summary = viewModel.ExtendedProps.Summary,
                        UsageTime = viewModel.ExtendedProps.UsageTime,
                        CategoryMasterId = viewModel.ExtendedProps.CategoryMasterId
                    };

                    db.Schedules.Add(schedule);
                    await db.SaveChangesAsync();

                    var result = await db.Schedules
                        .AsNoTracking()
                        .Where(x => x.Name == schedule.Name 
                            && x.UserId == schedule.UserId && x.UsageTime == schedule.UsageTime
                            && x.StartDay == schedule.StartDay && x.EndDay == schedule.EndDay)
                        .FirstOrDefaultAsync();

                    data.ScheduleId = result.Id;
                    await Add(data);
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

        public async Task<bool> InsertCalendarFromSchedule(long scheduleId, long userId)
        {
            if (scheduleId == 0) return false;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var data = await db.Schedules
                        .AsNoTracking()
                        .Where(x => x.UserId == userId && x.Id == scheduleId)
                        .FirstOrDefaultAsync();

                    DB.Calendar calendar = new DB.Calendar
                    {
                        UserId = userId,
                        CreateDate = DateTime.Now,
                        Title = data.Name,
                        Start = data.StartDay,
                        End = data.EndDay,
                        AllDay = false, // スケジュール作成では終日はなし
                        ScheduleId = data.Id
                    };

                    await Add(calendar);
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

        public async Task<bool> UpdateCalendar(CalendarEventViewModel viewModel, long userId)
        {
            if (viewModel == null) return false;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var data = await db.Calendars
                        .AsNoTracking()
                        .Where(x => x.UserId == userId && x.Id == viewModel.Id && x.Title == viewModel.Title)
                        .FirstOrDefaultAsync();
                    data.Start = viewModel.Start;
                    data.End = viewModel.End;
                    data.AllDay = viewModel.AllDay;
                    data.UpdateDate = DateTime.Now;

                    Update(data);
                    await db.SaveChangesAsync();

                    var schedule = await db.Schedules
                        .AsNoTracking()
                        .Where(x => x.Id == data.ScheduleId && x.UserId == userId && x.Name == viewModel.Title)
                        .FirstOrDefaultAsync();
                    schedule.Summary = viewModel.ExtendedProps.Summary;
                    schedule.StartDay = (DateTime)viewModel.Start;
                    schedule.EndDay = (DateTime)viewModel.End;
                    schedule.UsageTime = viewModel.ExtendedProps.UsageTime;
                    schedule.CategoryMasterId = viewModel.ExtendedProps.CategoryMasterId;
                    schedule.UpdateDate = DateTime.Now;

                    db.Update(schedule);
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

        public async Task DeleteCalendar(long calendarId)
        {
            if (calendarId == 0) return;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var calendar = await db.Calendars
                        .AsNoTracking()
                        .Where(x => x.Id == calendarId)
                        .FirstOrDefaultAsync();
                    var schedule = await db.Schedules
                        .AsNoTracking()
                        .Where(x => x.Id == calendar.ScheduleId)
                        .FirstOrDefaultAsync();

                    await Remove(calendarId);
                    await db.SaveChangesAsync();

                    if (schedule != null)
                    {
                        db.Schedules.Remove(schedule);
                        await db.SaveChangesAsync();
                    }

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
