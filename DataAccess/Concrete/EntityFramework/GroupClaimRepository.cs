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
    public class GroupClaimRepository : EfEntityRepositoryBase<GroupClaim, ProjectDbContext>, IGroupClaimRepository
    {
        public GroupClaimRepository(ProjectDbContext context):base(context)
        {

        }
        public async Task BulkInsert(int groupId, IEnumerable<GroupClaim> groupClaims)
        {
            var dbList = await context.GroupClaims.Where(x => x.GroupId == groupId).ToListAsync();

            context.GroupClaims.RemoveRange(dbList);
            await context.GroupClaims.AddRangeAsync(groupClaims);
        }

        public async Task<IEnumerable<SelectionItem>> GetGroupClaimsSelectedList(int groupId)
        {
            var list = await (from groupClaim in context.GroupClaims
                              join operationClaim in context.OperationClaims on groupClaim.ClaimId equals operationClaim.Id
                              where groupClaim.GroupId == groupId
                              select new SelectionItem()
                              {
                                  Id = operationClaim.Id.ToString(),
                                  Label = operationClaim.Name
                              }).ToListAsync();
            return list;
        }
    }
}
