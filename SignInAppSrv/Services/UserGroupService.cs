using Microsoft.AspNetCore.Mvc;
using SignInAppSrv.dbContext;
using SignInAppSrv.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignInAppSrv.Services
{
    public class UserGroupService : IRepository<GroupMembership>
    {
        DataContext _db;

        public UserGroupService(DataContext db)
        {
            this._db = db;
        }

        public GroupMembership CreateAsync(GroupMembership item)
        {
            if(_db.GroupMemberships.Any(x => x.UserId == item.UserId && x.GroupId == item.GroupId))
            {
                return null;
            }
            _db.Add(item);
            _db.SaveChanges();
            return item;
        }

        public Task<bool> DeleteAsync(int id)
        {
            var item = _db.GroupMemberships.First(x => x.Id == id);
            _db.Remove(item);
            _db.SaveChangesAsync();
            return Task.FromResult(true);
        }

        public Task<IEnumerable<GroupMembership>> GetAllAsync()
        {
            return Task.FromResult(_db.GroupMemberships.AsEnumerable<GroupMembership>());
        }

        public IEnumerable<GroupMembership> GetByGroupId(int id)
        {
            return _db.GroupMemberships.Where(x => x.GroupId == id);
        }

        public IEnumerable<GroupMembership> GetByUserId(int id)
        {
            return _db.GroupMemberships.Where(x => x.UserId == id);
        }

        public Task<GroupMembership> UpdateAsync(int id, GroupMembership item)
        {
            throw new NotImplementedException();
        }

        Task<GroupMembership> IRepository<GroupMembership>.CreateAsync(GroupMembership item)
        {
            throw new NotImplementedException();
        }

        Task<GroupMembership> IRepository<GroupMembership>.GetByGroupId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
