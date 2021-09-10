using SignInAppSrv.dbContext;
using SignInAppSrv.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace SignInAppSrv.Services
{
    public class DayService : IRepository<Day>
    {
        DataContext _db;

        public DayService(DataContext db)
        {
            this._db = db;
        }

        public Task<Day> CreateAsync(Day item)
        {
            return null;
        }

        public Task<bool> DeleteAsync(int id)
        {
            return null;
        }

        public Task<IEnumerable<Day>> GetAllAsync()
        {
            return Task.FromResult(_db.Days.AsEnumerable<Day>());
        }

        public Task<Day> GetByGroupId(int id)
        {
            return Task.FromResult(_db.Days.FirstOrDefault(x => x.Id == id));
        }

        public Task<Day> UpdateAsync(int id, Day item)
        {
            return null;
        }
    }
}
