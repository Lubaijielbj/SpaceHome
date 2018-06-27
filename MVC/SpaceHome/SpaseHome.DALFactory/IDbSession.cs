using SpaseHome.IDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaseHome.DALFactory
{
    public interface IDbSession
    {
        DbContext dbContext { get; }
        IAccountDAL AccountDAL { get; set; }
        IUserInfoDAL UserInfoDAL { get; set; }

        int SaveChanges();
    }
}
