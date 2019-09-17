using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Tameenk.Identity.DAL
{
    public partial class AspNetUsers: IdentityUser
    {
        public AspNetUsers()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            AspNetUserTokens = new HashSet<AspNetUserTokens>();
        }
        public string Token { get; set; }
        public string CompanyCrNumber { get; set; }
        public string CompanyVatNumber { get; set; }
        public string CompanySponserId { get; set; }
        public Guid LanguageId { get; set; }
        public Guid RoleId { get; set; }
        public bool IsCompany { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
    }
   
}
