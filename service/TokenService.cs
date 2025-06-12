using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simpleRESTApi.Service
{
    internal class TokenService
    {
        private readonly AspUserEF _aspuserEf;
        private ApplicationDbContext _context;
        public TokenService(AspUserEf _aspuserEf)
        {
            _aspuserEf = AspUserEf;
        }
        public string GenerateToken(string username)
        {
            var user = _aspuserEf.GetUserByUsername(username);
            if (user == null)
            {
                throw new KeyNotFoundExpection($"user with username'{username}' not found")
            }
            List<claim> claims = new List<Claim>
            {
                new Claim(ClaimType.Name,User.UserName)
            }
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var key = helpers.ApiSettings.SecretKeyBytes;
        }
        public bool ValidationToken(string token)
        {

        }

    }
}