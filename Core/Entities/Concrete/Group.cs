using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Group:IEntity
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
    }
}
