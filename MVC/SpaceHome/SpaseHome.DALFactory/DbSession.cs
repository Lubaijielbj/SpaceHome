using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaseHome.DAL;
using SpaseHome.IDAL;

namespace SpaseHome.DALFactory
{
    public class DbSession : IDbSession
    {
        public DbContext dbContext => DbContextFactory.CreatDbContext();

        /// <summary>
        /// 创建AccountDAL实例
        /// </summary>
        private IAccountDAL _accountDAL;
        public IAccountDAL AccountDAL {
            get
            {
                if (_accountDAL == null)
                {
                    _accountDAL = new AccountDAL();
                }
                return _accountDAL;
            }
            set
            {
                _accountDAL = value;
            }
        }

        /// <summary>
        /// 创建UserInfoDAL实例
        /// </summary>
        private IUserInfoDAL _userInfoDAL;
        public IUserInfoDAL UserInfoDAL
        {
            get
            {
                if (_userInfoDAL == null)
                {
                    _userInfoDAL = new UserInfoDAL();
                }

                return _userInfoDAL;
            }

            set
            {
                _userInfoDAL = value;
            }
        }


        /// <summary>
        /// 统一访问数据库
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            try
            {
                return dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
