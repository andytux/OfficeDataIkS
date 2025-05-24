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

        /// <summary>
        /// Holt sihc aus den ersten 8 zeichen der guid das salz
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public string GetSaltOfGuid(Guid employeeId)
        {
            return employeeId.ToString().Substring(0, 8);
        }

        /// <summary>
        /// Erstellt ein hashbares byte[] aus passwort und salz 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private byte[] GetHashableBytes(string password, string salt)
        {
            var combined = password + salt;

            return Encoding.UTF8.GetBytes(combined);
        }

        /// <summary>
        /// Erstellt einen passworthash aus passwort und salz
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public string GetPasswordHash(string password, string salt)
        {
            var hashableBytes = GetHashableBytes(password, salt);

            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(hashableBytes);

                return Convert.ToBase64String(hashBytes);
            }
        }

        /// <summary>
        /// Überprüft ob der hash des eingegebenen passworts mit dem gespeicherten hash übereinstimmt
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedHash"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
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
