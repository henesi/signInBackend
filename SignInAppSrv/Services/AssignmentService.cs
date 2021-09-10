using Microsoft.EntityFrameworkCore;
using SignInAppSrv.dbContext;
using SignInAppSrv.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignInAppSrv.Services
{
    public class AssignmentService : IRepository<Assignment>
    {
        DataContext _db;

        public AssignmentService(DataContext db)
        {
            this._db = db;
        }

        public Task<Assignment> CreateAsync(Assignment item)
        {
            _db.Add(item);
            _db.SaveChanges();
            return Task.FromResult(item);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var item = _db.Assignments.First(x => x.Id == id);
            _db.Remove(item);
            _db.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<IEnumerable<Assignment>> GetAllAsync()
        {
            return Task.FromResult(_db.Assignments.AsEnumerable<Assignment>());
        }

        public IEnumerable<Assignment> GetAll()
        {
            return _db.Assignments.Include(x => x.Day);
        }

        public IEnumerable<Assignment> GetByGroupId(int id)
        {
            return _db.Assignments.Include(x => x.Day).Where(x => x.GroupId == id);
        }

        public Task<Assignment> UpdateAsync(int id, Assignment item)
        {
            item.Id = id;
            _db.Assignments.Update(item);
            _db.SaveChanges();
            return Task.FromResult(item);
        }

        Task<Assignment> IRepository<Assignment>.GetByGroupId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
