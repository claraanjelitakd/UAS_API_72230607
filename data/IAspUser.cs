using System.Collections.Generic;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public interface IAspUser
    {
        IEnumerable<AspUser> GetAllUsers();
        AspUser RegisterUser(AspUser user);
        AspUser UpdateUser(AspUser user);
        AspUser GetUserByUsername(string username);
        void DeleteUser(string username);
        bool Login(string username, string password);
        bool ResetPassword(string username, string newPassword);
    }
}
