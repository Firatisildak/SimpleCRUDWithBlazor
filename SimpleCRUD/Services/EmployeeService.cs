using Microsoft.EntityFrameworkCore;
using SimpleCRUD.DataAccess;
using SimpleCRUD.ViewModels;

namespace SimpleCRUD.Services
{
    public class EmployeeService
    {
        private readonly AppDBContext dBContext;

        public EmployeeService(AppDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task<List<EmployeeViewModel>> GetAllEmployees()
        {
            return await dBContext.Employees
                .OrderBy(x => x.FullName)
                .Select(x => new EmployeeViewModel
                {
                    EmployeeId = x.EmployeeId,
                    FullName = x.FullName,
                    Department = x.Department,
                    DateOfBirth = x.DateOfBirth,
                    Age = x.Age,
                    PhoneNumber = x.PhoneNumber,
                }).ToListAsync();
        }
    }
}
