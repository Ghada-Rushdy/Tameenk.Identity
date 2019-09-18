using System;
using System.Collections.Generic;
using System.Text;
using Tameenk.Identity.Log.DAL;

namespace Tameenk.Identity.Log.DAL
{
    public class AuthenticationLogRepository : GenericRepository<AuthenticationLog, int>, IAuthenticationLogRepository
    {
        public AuthenticationLogRepository(LogContext dbContext): base(dbContext)
        {
        }
        public string vvv()
        {
            throw new NotImplementedException();
        }
    }
}
