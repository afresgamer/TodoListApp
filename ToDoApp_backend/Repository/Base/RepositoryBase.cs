using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.Context;
using ToDoApp_backend.DB.Base;

namespace ToDoApp_backend.Repository.Base
{
    public class RepositoryBase<T> where T : class, IDbBase
    {
        protected readonly MainContext db;
        protected readonly DbSet<T> dbSet;
        public RepositoryBase(MainContext db)
        {
            this.db = db;
            this.dbSet = this.db.Set<T>();
        }

        public virtual async Task<T> FindAsync(long id)
        {
            if (id <= 0) return null;
            return await dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task Add(T data)
        {
            if (data == null) return;
            await dbSet.AddAsync(data);
        }

        public virtual async Task Add(IList<T> dataList)
        {
            if (dataList == null) return;
            foreach (var data in dataList)
            {
                await Add(data);
            }
        }

        public virtual void Update(T data)
        {
            if (data == null) return;
            dbSet.Update(data);
        }

        public virtual void Update(IList<T> dataList)
        {
            if (dataList == null) return;
            foreach (var data in dataList)
            {
                Update(data);
            }
        }

        public virtual void Remove(T data)
        {
            if (data == null) return;
            dbSet.Remove(data);
        }

        public virtual void Remove(IList<T> dataList)
        {
            if (dataList == null) return;
            foreach (var data in dataList)
            {
                Remove(data);
            }
        }

        public virtual async Task Remove(long id)
        {
            if (id <= 0) return;
            var m = await FindAsync(id);
            dbSet.Remove(m);
        }

        public virtual async Task Remove(IList<long> idList)
        {
            if (idList == null) return;
            foreach (var id in idList)
            {
                await Remove(id);
            }
        }
    }
}
