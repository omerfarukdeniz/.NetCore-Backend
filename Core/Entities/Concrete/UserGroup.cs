using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class UserGroup:IEntity
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
    }
}
