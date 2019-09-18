
using System;
using System.ComponentModel.DataAnnotations;

namespace Tameenk.Identity.Log.DAL
{
    public class AuthenticationLog
    {
        [Key]
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public ErrorCodes ErrorCode { get; set; }
        public string ErrorDescription { get; set;}
        public string Method { get; set; }
        public string ServerIP { get; set; }
        public int? Channel { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }        public string CompanyCrNumber { get; set; }        public string CompanyVatNumber { get; set; }        public string CompanySponserId { get; set; }

    }
}
