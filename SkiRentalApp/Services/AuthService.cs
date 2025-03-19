using System.Security.Cryptography;
using System.Text;

namespace SkiRentalApp.Services
{
    public class AuthService
    {
        private readonly EmployeeService employeeService;

        public AuthService(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string GetSaltOfGuid(Guid employeeId)
        {
            return employeeId.ToString().Substring(0, 8);
        }

        private byte[] GetHashableBytes(string password, string salt)
        {
            var combined = password + salt;

            return Encoding.UTF8.GetBytes(combined);
        }

        public string GetPasswordHash(string password, string salt)
        {
            var hashableBytes = GetHashableBytes(password, salt);

            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(hashableBytes);

                return Convert.ToBase64String(hashBytes);
            }
        }

        public bool VerifyPassword(string password, string storedHash, string salt)
        {
            var hashOfInput = GetPasswordHash(password, salt);

            if (hashOfInput != storedHash)
            {
                return false;
            }

            return true;
        }
    }
}
