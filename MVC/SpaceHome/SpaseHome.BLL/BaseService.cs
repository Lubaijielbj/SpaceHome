using SpaseHome.DALFactory;
using SpaseHome.IBLL;
using SpaseHome.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpaseHome.BLL
{
    public abstract class BaseService<T> where T : class, new()
    {
        public IDbSession dbSession = DbSessionFactory.CreatDbSession();

        private IBaseDAL<T> CurrentDAL;
        public abstract IBaseDAL<T> SetCurrentDAL();
        public BaseService()
        {
            CurrentDAL = SetCurrentDAL();
        }

        public T AddEnity(T enity)
        {
            return CurrentDAL.AddEnity(enity);
        }

        public bool DelectEntity(T enity)
        {
            return CurrentDAL.DelectEntity(enity);
        }

        public T EditEnity(T enity)
        {
            return CurrentDAL.EditEnity(enity);
        }

        public IQueryable<T> LoadEnities(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDAL.LoadEnities(whereLambda);
        }
    }
}
