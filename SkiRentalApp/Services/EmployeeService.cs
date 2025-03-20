using SkiRentalApp.Data.Models;
using SkiRentalApp.Data;
using Microsoft.EntityFrameworkCore;

namespace SkiRentalApp.Services
{
    public class EmployeeService
    {
        private readonly AppDbContext dbContext;

        public EmployeeService(AppDbContext dbContext )
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Fügt Mitarbeiter hinzu
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task AddEmployeeAsync(Employee employee)
        {
            dbContext.Employees.Add(employee);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Gibt den Mitarbeiter mit dem Usernamen zurück
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<Employee?> GetEmployeeByNameAsync(string userName)
        {
            return await dbContext.Employees.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        /// <summary>
        /// Prüft ob der Username schon existiert
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> EmloyeeAlreadyExistsAsync(string userName)
        {
            return await dbContext.Employees.AnyAsync(u => u.UserName == userName);
        }
    }
}
