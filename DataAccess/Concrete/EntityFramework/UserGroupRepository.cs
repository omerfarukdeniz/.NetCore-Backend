using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class UserGroupRepository : EfEntityRepositoryBase<UserGroup, ProjectDbContext>, IUserGroupRepository
    {
        public UserGroupRepository(ProjectDbContext context):base(context)
        {

        }
        public async Task BulkInsert(int userId, IEnumerable<UserGroup> userGroups)
        {
            var dbUserGroupGroupList = context.UserGroups.Where(x => x.UserId == userId);

            context.UserGroups.RemoveRange(dbUserGroupGroupList);
            await context.UserGroups.AddRangeAsync(userGroups);
        }

        public async Task BulkInsertByGroupId(int groupId, IEnumerable<UserGroup> userGroups)
        {
            var dbUserGroupList = context.UserGroups.Where(x => x.GroupId == groupId);

            context.UserGroups.RemoveRange(dbUserGroupList);
            await context.UserGroups.AddRangeAsync(userGroups);
        }

        public async Task<IEnumerable<SelectionItem>> GetUserGroupSelectedList(int userId)
        {
            var list = await (from groups in context.Groups
                              join userGroup in context.UserGroups on groups.Id equals userGroup.GroupId
                              where userGroup.UserId == userId
                              select new SelectionItem()
                              {
                                  Id = groups.Id.ToString(),
                                  Label = groups.GroupName
                              }).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<SelectionItem>> GetUsersInGroupSelectedListByGroupId(int groupId)
        {
            var list = await (from user in context.Users
                              join groupUser in context.UserGroups on user.UserId equals groupUser.UserId
                              where groupUser.GroupId == groupId
                              select new SelectionItem()
                              {
                                  Id = user.UserId.ToString(),
                                  Label = user.FirstName + user.LastName
                              }).ToListAsync();

            return list;
        }
    }
}
