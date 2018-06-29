using SpaseHome.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SpaseHome.DAL
{
    /// <summary>
    /// 创建线程内唯一的DbContext
    /// </summary>
    public class DbContextFactory
    {
        public static DbContext CreatDbContext()
        {
            DbContext dbContext = (DbContext)CallContext.GetData("dbContext");
            if (dbContext == null)
            {
                dbContext = new SpaseHomeEntities();
                CallContext.SetData("dbContext", dbContext);
            }

            return dbContext;
        }
    }
}
