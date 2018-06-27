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
    public class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        public override IBaseDAL<UserInfo> SetCurrentDAL()
        {
            return dbSession.UserInfoDAL;
        }
    }
}
