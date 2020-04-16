using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tameenk.Identity.DAL
{
    [JsonObject("login")]
    public class LoginModel: BaseModel
    {
        [JsonProperty("username")]     
        public string UserName { get; set; }

        [JsonProperty("password")]
        [JsonRequired]
        public string Password { get; set; }

        [JsonProperty("email")]
        [JsonRequired]
        public string Email { get; set; }
   
    }
}
