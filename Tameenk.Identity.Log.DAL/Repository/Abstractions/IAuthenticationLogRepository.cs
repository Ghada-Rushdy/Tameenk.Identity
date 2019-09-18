using System;
using System.Collections.Generic;
using System.Text;

namespace Tameenk.Identity.Log.DAL
{
    public interface IAuthenticationLogRepository : IGenericRepository< AuthenticationLog, int>
    {
        string vvv();
    }
}
