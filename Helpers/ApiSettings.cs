using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Helpers
{
    public class ApiSettings
    {
        internal static string Secretkey (get; set;) = "has password";
        internal static byte[] SecretKeyBytes = System.Text.Encoding.UTFB.GetBytes(SecretKey);
    }
}