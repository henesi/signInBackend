using SignInAppSrv.dbContext;
using SignInAppSrv.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SignInAppSrv.Services
{
    public class GroupService : IRepository<Group>
    {
        DataContext _db;

        public GroupService(DataContext db)
        {
            this._db = db;
        }

        public Group CreateAsync(Group item)
        {
            if(_db.Groups.Any(x => x.Identifier == item.Identifier)) { return null; }
            _db.Add(item);
            _db.SaveChanges();
            return item;
        }

        public Task<bool> DeleteAsync(int id)
        {
            var item = _db.Groups.First(x => x.Id == id);
            _db.Remove(item);
            _db.SaveChanges();
            return Task.FromResult(true);
        }

        public IQueryable<Group> GetAll()
        {
            return _db.Groups.Include(x => x.groupMemberships);

        }


        public Group GetById(int id)
        {
            var group = _db.Groups.Find(id);
            return group;
        }

        public Task<Group> GetByIdentifier(string identifier)
        {
            if (!_db.Groups.Any(x => x.Identifier == identifier))  { return null; }
            return Task.FromResult(_db.Groups.First(x => x.Identifier == identifier));
        }

        public Task<Group> UpdateAsync(int id, Group item)
        {
            var itemOld = _db.Groups.FirstOrDefault(x => x.Id == id);
            if (_db.Groups.FirstOrDefault(x => x.Identifier == item.Identifier && x.Id != item.Id) != null)
            {
                return null;
            }
            itemOld.Identifier = item.Identifier;
            itemOld.Name = item.Name;
            _db.Groups.Update(itemOld);
            _db.SaveChangesAsync();
            return Task.FromResult(itemOld);
        }

        Task<Group> IRepository<Group>.CreateAsync(Group item)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Group>> IRepository<Group>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Group> IRepository<Group>.GetByGroupId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
