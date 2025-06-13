using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using UAS_POS_CLARA.Models;
using Microsoft.AspNetCore.WebUtilities;
using UAS_POS_CLARA.Helpers;

namespace UAS_POS_CLARA.Data
{
    public class AspUserEF : IAspUser
    {
        private readonly ApplicationDbContext _context;

        public AspUserEF(ApplicationDbContext context)
        {
            _context = context;
        }

        public AspUser Authenticate(string username, string password)
        {
            var user = _context.AspUsers.FirstOrDefault(u => u.Username == username);
            if (user == null) return null;

            if (!HashHelper.VerifyPassword(password, user.Password))
                return null;

            return user;
        }

        public void DeleteUser(string username)
        {
            try
            {
                var user = _context.AspUsers.Find(username);
                if (user != null)
                {
                    _context.AspUsers.Remove(user);
                    _context.SaveChanges();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency issues (e.g., log the error or rethrow)
                throw new Exception("Concurrency error occurred while deleting user", ex);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                throw new Exception("Error deleting user", ex);
            }
        }

        public string GenerateToken(AspUser username)
        {
            var user = GetUserByUsername(username.Username);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with username not found.");
            }
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var key = Helpers.ApiSettings.GenerateSecretBytes();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public IEnumerable<AspUser> GetAllUsers()
        {
            var users = _context.AspUsers.OrderBy(u => u.Username).ToList();
            if (users == null || !users.Any())
            {
                throw new InvalidOperationException("No users found in the database.");
            }
            return users;

        }

        public AspUser GetUserByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username cannot be null or empty", nameof(username));
            }
            var user = _context.AspUsers.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with username '{username}' not found.");
            }
            return user;
        }

        public async Task<AspUser?> GetUserByUsernameAsync(string username)
        {
            return await _context.AspUsers.FirstOrDefaultAsync(u => u.Username == username);
        }

        public bool LoginUser(string username, string password)
        {
            var user = _context.AspUsers.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return false;
            }
            var hashed = Helpers.HashHelper.HashPassword(password);
            return user.Password == hashed;
        }

        public AspUser RegisterUser(AspUser user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "User cannot be null");
                }
                user.Password = Helpers.HashHelper.HashPassword(user.Password);
                _context.AspUsers.Add(user);
                _context.SaveChanges();
                return user;
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions (e.g., log the error or rethrow)
                throw new Exception("Error registering user", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions (e.g., log the error)
                throw new Exception("An error occurred while registering the user", ex);
            }
        }

        public AspUser UpdateUser(AspUser user)
        {
            var existingUser = _context.AspUsers.Find(user.Username);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with username '{user.Username}' not found.");
            }
            existingUser.Email = user.Email;
            existingUser.Password = Helpers.HashHelper.HashPassword(user.Password);
            _context.SaveChanges();
            return existingUser;
        }
    }
}