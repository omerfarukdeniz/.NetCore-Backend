using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Fakes
{
    public interface IFakeStore
    {
        List<TEntity> Set<TEntity>();
    }
}
