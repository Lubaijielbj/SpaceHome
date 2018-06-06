using SpaseHome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaseHome.IBLL
{
    public interface IAccountService:IBaseService<Account>
    {
        string Test();
    }
}
