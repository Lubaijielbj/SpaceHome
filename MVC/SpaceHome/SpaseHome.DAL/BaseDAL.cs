using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaseHome.DAL
{
    public class BaseDAL<T> where T:class,new()
    {
        DbContext dbContext = DbContextFactory.CreatDbContext();

        public T AddEnity(T enity)
        {
            dbContext.Set<T>().Add(enity);
            var TEnity = LoadEnities(t => t == enity);
            return TEnity.FirstOrDefault<T>();
        }

        public bool DelectEntity(T enity)
        {
            dbContext.Entry<T>(enity).State = EntityState.Deleted;
            return true;
        }

        public T EditEnity(T enity)
        {
            dbContext.Entry<T>(enity).State = EntityState.Modified;
            var TEnity = LoadEnities(t => t == enity);
            return TEnity.FirstOrDefault<T>();
        }

        public IQueryable<T> LoadEnities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return dbContext.Set<T>().Where<T>(whereLambda);
        }
    }
}
