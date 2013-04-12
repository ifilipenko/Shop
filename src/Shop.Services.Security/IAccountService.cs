using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Security
{
    public interface IAccountService
    {
        void RegisterNewUser(string userName, string password);
    }
}
