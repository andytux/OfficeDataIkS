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

        public async Task AddEmployeeAsync(Employee employee)
        {
            dbContext.Employees.Add(employee);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Employee?> GetEmployeeByNameAsync(string userName)
        {
            return await dbContext.Employees.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<bool> EmloyeeAlreadyExistsAsync(string userName)
        {
            return await dbContext.Employees.AnyAsync(u => u.UserName == userName);
        }
    }
}
