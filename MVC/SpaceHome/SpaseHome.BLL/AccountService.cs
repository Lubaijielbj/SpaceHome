using SpaseHome.IBLL;
using SpaseHome.IDAL;
using SpaseHome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpaseHome.BLL
{
    public class AccountService : BaseService<Account>, IAccountService
    {
        /// <summary>
        /// 查询数据库中是否存在数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns>如果存在返回true,否则返回false</returns>
        public bool QueryEnity(Expression<Func<Account, bool>> whereLambda)
        {
            var accounts = dbSession.AccountDAL.LoadEnities(whereLambda);
            if (accounts.Count<Account>() > 0)
            {       
                return true;
            }
            else
            {
                return false;
            }
        }

        public override IBaseDAL<Account> SetCurrentDAL()
        {
            return dbSession.AccountDAL;
        }        
    }
}
