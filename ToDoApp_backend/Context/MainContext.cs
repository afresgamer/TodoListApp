using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.DB;

namespace ToDoApp_backend.Context
{
    public class MainContext: DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<CategoryMaster> CategoryMasters { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
    }
}
