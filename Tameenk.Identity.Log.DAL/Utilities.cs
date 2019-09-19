using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Tameenk.Identity.Log.DAL
{
    public class Utilities
    {
        public static string GetServerIP()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                return (from ip in host.AddressList where ip.AddressFamily == AddressFamily.InterNetwork select ip.ToString()).FirstOrDefault();
            }
            catch (Exception exp)
            {
                // ErrorLogger.LogError(exp.Message, exp, false);
                return string.Empty;
            }
        }

    }
}
