﻿using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class MobileLoginRepository : EfEntityRepositoryBase<MobileLogin, ProjectDbContext>, IMobileLoginRepository
    {
        public MobileLoginRepository(ProjectDbContext context) : base(context)
        {

        }
    }
}
