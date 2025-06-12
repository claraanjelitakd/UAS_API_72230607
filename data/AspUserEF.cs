using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class AspUserEF : IAspUser
    {
        private readonly ApplicationDbContext _context;

        public AspUserEF(ApplicationDbContext context)
        {
            _context = context;
        }
        public void DeleteUser(string username)
        {
            var user = _context.AspUsers.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                _context.AspUsers.Remove(user);
                _context.SaveChanges();
            }
        }

        public IEnumerable<AspUser> GetAllUsers()
        {
            return _context.AspUsers.ToList();
        }

        public AspUser GetUserByUsername(string username)
        {
            return _context.AspUsers.FirstOrDefault(u => u.Username == username);
        }

        public bool Login(string username, string password)
        {
         var user = _context.AspUsers.FirstOrDefault(u => u.Username == username && u.password == password);
         if (user == null)
         {
            return false;
         }
         var password= Helpers.HashHelper.HashPassword(password);
         if (user.Password == pass)
         {
            return true;
         }
         return falase;
        }

        public AspUser RegisterUser(AspUser user)
        {
            _context.AspUsers.Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool ResetPassword(string username, string newPassword)
        {
            var user = _context.AspUsers.FirstOrDefault(u => u.Username == username);
            if (user == null) return false;

            user.password = newPassword;
            _context.SaveChanges();
            return true;
        }

        public AspUser UpdateUser(AspUser user)
        {
            var existingUser = _context.AspUsers.FirstOrDefault(u => u.Username == user.Username);
            if (existingUser == null) return null;

            existingUser.password = user.password;
            existingUser.Email = user.Email;
            // Tambahkan field lain yang perlu diperbarui

            _context.SaveChanges();
            return existingUser;
        }
    }
}