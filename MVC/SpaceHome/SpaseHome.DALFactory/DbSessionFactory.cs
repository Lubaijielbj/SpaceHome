using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SpaseHome.DALFactory
{
    public class DbSessionFactory
    {
        /// <summary>
        /// 创建线程内唯一的DbSession
        /// </summary>
        /// <returns></returns>
        public static IDbSession CreatDbSession()
        {
            IDbSession dbSession = (IDbSession)CallContext.GetData("dbSession");
            if (dbSession == null)
            {
                dbSession = new DbSession();
                CallContext.SetData("dbSession", dbSession);
            }

            return dbSession;
        }
    }
}
