
using Microsoft.EntityFrameworkCore;
using StockIT.BLL.Services;
using StockIT.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using StockIT.BLL.Exceptions;

namespace StockIT.BLL.Services
{
    public class UserService : IUserService
    {

        private readonly StockITContext _context;

        public UserService(StockITContext context)
        {
            _context = context;
        }

        public User? RegisterUser(User? userDetails)
        {
            // 1. Input Validation (ensure Username is unique, etc.)

            // 2. Password Hashing 
            userDetails.Password = HashPassword(userDetails.Password);
            try
            {
                // 3. Save to Database
                _context.Users.Add(userDetails);
                _context.SaveChanges();

                return userDetails;
            }
            catch (DbUpdateException ex)
            {
                throw new UserServiceException($"Error saving user: {ex.Message}");
            }
        }


        public User AuthenticateUser(string username, string password)
        {
            // 1. Find user by Username
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            // 2. If user found, verify Password
            if (user != null && VerifyPasswordHash(password, user))
            {
                return user;
            }

            return null; // User not found or incorrect Password 
        }

        public User SetNewPassword(User user, string password)
        {
            if (user != null)
            {
                user.Password = HashPassword(password);
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    throw new UserServiceException($"Error saving user password: {ex.Message}");
                }
            }

            return user;
        }

        public void UpdateUserProfile(int userId, User userDetails)
        {
            var user = _context.Users.FirstOrDefault(u => u.ID == userId);
            if (user != null)
            {
                user.FirstName = userDetails.FirstName;
                user.LastName = userDetails.LastName;

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    throw new UserServiceException($"Error saving user password: {ex.Message}");
                }
            }
        }

        public bool IsAdmin(int userId)
        {
            throw new NotImplementedException();
        }

        public string HashPassword(string password)
        {
            // Install BCrypt.Net-Next NuGet Package
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPasswordHash(string password, User user)
        {
            var t = BCrypt.Net.BCrypt.HashPassword(password);

            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        public User? GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(user => user.Username == username);
        }

        public User GetUserProfile(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.ID == userId);
            if (user == null) return new User();
            return user;

        }
        public Task<User> GetUserProfileAsync(int userId)
        {
            var user = _context.Users.FirstOrDefaultAsync(u => u.ID == userId);
            if (user == null) throw new UserServiceException($"No user found"); ;
            return user;

        }
    }
}