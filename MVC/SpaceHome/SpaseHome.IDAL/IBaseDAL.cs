using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaseHome.IDAL
{
    public interface IBaseDAL<T> where T:class,new()
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IQueryable<T> LoadEnities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="enity"></param>
        /// <returns></returns>
        bool DelectEntity(T enity);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="enity"></param>
        /// <returns></returns>
        T AddEnity(T enity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="enity"></param>
        /// <returns></returns>
        T EditEnity(T enity);
    }
}
