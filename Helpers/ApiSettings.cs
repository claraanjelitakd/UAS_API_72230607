using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAS_POS_CLARA.Helpers
{
    public class ApiSettings
    {
        internal static string SecretKey = "C7A81920ajkdshaHDJKSAJDKJASD";
        internal static byte[] GenerateSecretBytes() =>
        Encoding.ASCII.GetBytes(SecretKey);
    }
}