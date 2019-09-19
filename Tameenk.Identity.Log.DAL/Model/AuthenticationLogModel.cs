
using System;

namespace Tameenk.Identity.Log.DAL
{
    public class AuthenticationLogModel
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public ErrorCodes ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public string Method { get; set; }
        public string ServerIP { get; set; }
        public int Channel { get; set; }
        public string Email { get; set; }
    }
}
