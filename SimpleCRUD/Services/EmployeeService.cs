using Microsoft.EntityFrameworkCore;
using SimpleCRUD.DataAccess;
using SimpleCRUD.DataAccess.Entities;
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

        public bool CreateNewEmployee(EmployeeViewModel model)
        {

            try
            {
                Employee employee = new Employee
                {
                    FullName = model.FullName,
                    Department = model.Department,
                    DateOfBirth = model.DateOfBirth,
                    Age = model.Age,
                    PhoneNumber = model.PhoneNumber
                };
                dBContext.Employees.Add(employee);
                var result=dBContext.SaveChanges();
                return result > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
