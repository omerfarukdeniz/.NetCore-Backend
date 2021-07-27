using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class GroupRepository:EfEntityRepositoryBase<Group, ProjectDbContext>,IGroupRepository
    {
        public GroupRepository(ProjectDbContext context):base(context)
        {

        }
    }
}
