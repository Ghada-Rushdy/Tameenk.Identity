using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tameenk.Identity.DAL
{
    public class ApplicationUser : IdentityUser
    {
        public string Token { get; set; }
        public string FullName { get; set; }
        public string CompanyCrNumber { get; set; }
        public string CompanyVatNumber { get; set; }
        public string CompanySponserId { get; set; }
        public Guid LanguageId { get; set; }
        public Guid RoleId { get; set; }
        public bool IsCompany { get; set; }
        public string Channel { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
