using Newtonsoft.Json;using System;using System.Collections.Generic;using System.ComponentModel.DataAnnotations;using System.Linq;using System.Threading.Tasks;using Tameenk.Identity.DAL;namespace Tameenk.Identity.DAL{    [JsonObject("register")]    public class RegisterCompanyModel : BaseModel    {        [JsonProperty("email")]        public string Email { get; set; }        [JsonProperty("confirmemail")]        public string ConfirmEmail { get; set; }        [JsonProperty("mobile")]        public string Mobile { get; set; }        [JsonProperty("password")]        public string Password { get; set; }        [JsonProperty("CompanyName")]        public string CompanyName { get; set; }        [JsonProperty("companycrnumber")]        public string CompanyCrNumber { get; set; }        [JsonProperty("companyvatnumber")]        public string CompanyVatNumber { get; set; }        [JsonProperty("companysponserid")]        public string CompanySponserId { get; set; }        public List<string> ModelErrors { get; set; }
        public bool IsCompany { get; set; }        public Channel Channel { get; set; }        public bool IsValid        {            get            {                ModelErrors = new List<string>();                if (string.IsNullOrEmpty(Email))                {                    ModelErrors.Add("Mail Not Exist");                }                if (string.IsNullOrEmpty(Mobile))                {                    ModelErrors.Add("Mobile Not Exist");                }                if (string.IsNullOrEmpty(Password))                {                    ModelErrors.Add("Password Not Exist");                }                if (string.IsNullOrEmpty(ConfirmEmail))                {                    ModelErrors.Add("ConfirmEmail Not Exist");                }                if (Channel == null)                {                    ModelErrors.Add("Channel Not Exist");                }                if (ModelErrors.Count() > 0)                {                    return false;                }                else                    return true;            }        }    }

    //public class RegisterViewModel : BaseModel
    //{
    //    [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "EmailRequired")]
    //    public string Email { get; set; }

    //    [Required(AllowEmptyStrings = false,  ErrorMessageResourceName = "CompanyNameRequired")]
    //    public string CompanyName { get; set; }

    //    [Required(AllowEmptyStrings = false,  ErrorMessageResourceName = "CompanyCRNumberRequired")]
    //    public string CompanyCRNumber { get; set; }

    //    [Required(AllowEmptyStrings = false,  ErrorMessageResourceName = "MobileNumberRequired")]
    //    public string MobileNumber { get; set; }


    //    [Required(AllowEmptyStrings = false,  ErrorMessageResourceName = "CompanyVatNumberRequired")]
    //    public string CompanyVatNumber { get; set; }


    //    [Required(AllowEmptyStrings = false,  ErrorMessageResourceName = "SponsorRequired")]
    //    public string CompanySponsorId { get; set; }


    //    [Required(AllowEmptyStrings = false,  ErrorMessageResourceName = "PasswordRequired")]
    //    public string Password { get; set; }

    //    [Required(AllowEmptyStrings = false,  ErrorMessageResourceName = "ConfirmPasswordRequired")]
    //    [Compare("Password",  ErrorMessageResourceName = "PasswordsNotMatches")]
    //    public string ConfirmPassword { get; set; }

    //}
}