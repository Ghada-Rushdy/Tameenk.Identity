using System;
using System.Collections.Generic;
using System.Text;

namespace Tameenk.Identity.Log.DAL
{
    public enum ErrorCodes
    {
        Success = 1,
        NullResponse,
        UnspecifiedError,
        ServiceError,
        NullRequest,
        NinIsNull,
        ServiceException,
        MethodException
    }
}
