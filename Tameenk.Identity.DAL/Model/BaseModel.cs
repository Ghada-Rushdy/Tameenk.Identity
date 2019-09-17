using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Tameenk.Identity.DAL
{
    public abstract class BaseModel
    {
        public string Language { get; set; } = "ar";
        public Channel Channel { get; set; } = Channel.Portal;
    }
}
