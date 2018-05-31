using SpaseHome.IBLL;
using SpaseHome.IDAL;
using SpaseHome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaseHome.BLL
{
    public class AccountService : BaseService<Account>, IAccountService
    {
        public override IBaseDAL<Account> SetCurrentDAL()
        {
            return dbSession.AccountDAL;
        }
    }
}
