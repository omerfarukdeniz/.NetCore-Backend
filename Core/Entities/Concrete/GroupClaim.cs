using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class GroupClaim:IEntity
    {
        public int GroupId { get; set; }
        public int ClaimId { get; set; }
    }
}