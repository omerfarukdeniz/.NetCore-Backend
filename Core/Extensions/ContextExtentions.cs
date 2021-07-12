using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Extensions
{
    public static class ContextExtentions
    {
        public static DbSet<T> Set<T>(this DbContext _context, Type t) where T: class
        {
            return (DbSet<T>)_context.GetType().GetMethod("Set").MakeGenericMethod(t).Invoke(_context, null);
        }

        public static IQueryable<T> QueryableOf<T>(this DbContext _context, string typeName) where T : class
        {
            var type = _context.Model.GetEntityTypes(typeName).First();
            var q = (IQueryable)_context
                .GetType()
                .GetMethod("Set")
                .MakeGenericMethod(type.ClrType)
                .Invoke(_context, null);
            return q.OfType<T>();
        }
    }
}
