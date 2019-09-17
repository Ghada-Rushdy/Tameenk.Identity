using System;
using System.Collections.Generic;
using System.Text;

namespace Tameenk.Identity.DAL
{
    public class LoginOutput
    {
        public enum ErrorCodes
        {
            Success = 1,
            NullResponse,
            UnspecifiedError,
            ServiceError,
            NullRequest,
            ServiceException,
            MethodException
        }

        public ErrorCodes ErrorCode
        {
            get;
            set;
        }
        public string ErrorDescription
        {
            get;
            set;
        }

        public string Token
        {
            get;
            set;
        }
    }
}
