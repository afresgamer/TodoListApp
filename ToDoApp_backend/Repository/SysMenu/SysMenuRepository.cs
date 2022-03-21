using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.Context;
using ToDoApp_backend.Repository.Base;

namespace ToDoApp_backend.Repository.SysMenu
{
    public interface ISysMenuRepository
    {
        Task<DB.SysMenu> FindSysMenuAsync(long userId, string name);
        Task<List<DB.SysMenu>> FetchSysMenuAsync(long userId);
        Task<List<string>> FetchSysMenuNameAsync(long userId);
        Task<bool> InsertSysMenu(long userId);
    }

    public class SysMenuRepository : RepositoryBase<DB.SysMenu>, ISysMenuRepository
    {
        private readonly IMapper _mapper;

        public SysMenuRepository(MainContext db, IMapper mapper) : base(db)
        {
            _mapper = mapper;
        }

        public async Task<DB.SysMenu> FindSysMenuAsync(long userId, string name)
        {
            var result = await db.SysMenus
                .AsNoTracking()
                .Where(x => x.UserId == userId && x.Name == name)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<DB.SysMenu>> FetchSysMenuAsync(long userId)
        {
            var result = await db.SysMenus
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return result;
        }

        public async Task<List<string>> FetchSysMenuNameAsync(long userId)
        {
            var result = await db.SysMenus
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => x.Name)
                .ToListAsync();

            return result;
        }

        //初期レコードを追加
        public async Task<bool> InsertSysMenu(long userId)
        {
            if (userId == 0) return false;

            var dataList = new List<DB.SysMenu>
            {
                new DB.SysMenu{ CreateDate = DateTime.Now, Name = "schedule", UserId = userId, SortNo = 1 },
                new DB.SysMenu{ CreateDate = DateTime.Now, Name = "calender", UserId = userId, SortNo = 2 },
                new DB.SysMenu{ CreateDate = DateTime.Now, Name = "categoryMaster", UserId = userId, SortNo = 3 },
                new DB.SysMenu{ CreateDate = DateTime.Now, Name = "setting", UserId = userId, SortNo = 4 },
                new DB.SysMenu{ CreateDate = DateTime.Now, Name = "home", UserId = userId, SortNo = 5 }
            };

            using (var transaction = db.Database.BeginTransaction())
            {
                await Add(dataList);

                await db.SaveChangesAsync();
                transaction.Commit();

                return true;
            }
        }
    }
}
