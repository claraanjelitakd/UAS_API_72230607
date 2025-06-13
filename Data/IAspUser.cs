using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAS_POS_CLARA.Models;

namespace UAS_POS_CLARA.Data
{
    public interface IAspUser
    {
        IEnumerable<AspUser> GetAllUsers();
        AspUser GetUserByUsername(string username);
        AspUser RegisterUser(AspUser user);
        AspUser UpdateUser(AspUser user);
        void DeleteUser(string username);
        bool LoginUser(string username, string password);
        string GenerateToken(AspUser username);
        AspUser Authenticate(string username, string password);

    }
}