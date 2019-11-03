using FCTAssignmentApp.Data;
using FCTAssignmentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCTAssignmentApp.Services
{
    public interface IUserService
    {
        Customer Authenticate(string username, string password);
        IEnumerable<Customer> GetAll();
        Customer GetById(int id);
        Customer Create(Customer user, string password);
        void Update(Customer user, string password = null);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private CustomerContext _context;

        public UserService(CustomerContext context)
        {
            _context = context;
        }

        public Customer Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Customers.SingleOrDefault(x => x.Email == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers;
        }

        public Customer GetById(int id)
        {
            return _context.Customers.Find(id);
        }

        public Customer Create(Customer user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            if (_context.Customers.Any(x => x.Email == user.Email))
                throw new Exception("Email \"" + user.Email + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Customers.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(Customer userParam, string password = null)
        {
            var user = _context.Customers.Find(userParam.Id);

            if (user == null)
                throw new Exception("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.Email) && userParam.Email != user.Email)
            {
                // throw error if the new username is already taken
                if (_context.Customers.Any(x => x.Email == userParam.Email))
                    throw new Exception("Email " + userParam.Email + " is already taken");

                user.Email = userParam.Email;
            }

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.Name))
                user.Name = userParam.Name;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Customers.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Customers.Find(id);
            if (user != null)
            {
                _context.Customers.Remove(user);
                _context.SaveChanges();
            }
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
