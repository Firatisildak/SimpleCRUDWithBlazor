using ClosedXML.Excel;
using EFCore.BulkExtensions;
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
                var result = dBContext.SaveChanges();
                return result > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public EmployeeViewModel? FindEmployee(int employeeId)
        {
            var employee = dBContext.Employees.Find(employeeId);
            if (employee == null) return null;
            EmployeeViewModel result = new EmployeeViewModel
            {
                EmployeeId = employee.EmployeeId,
                FullName = employee.FullName,
                Department = employee.Department,
                DateOfBirth = employee.DateOfBirth,
                Age = employee.Age,
                PhoneNumber = employee.PhoneNumber
            };
            return result;
        }

        public bool UpdateEmployee(EmployeeViewModel model)
        {
            try
            {
                var employee = dBContext.Employees.Find(model.EmployeeId);
                if (employee == null) return false;

                employee.FullName = model.FullName;
                employee.Department = model.Department;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Age = model.Age;
                employee.PhoneNumber = model.PhoneNumber;

                var result = dBContext.SaveChanges();
                return result > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteEmployee(int employeeId)
        {
            try
            {
                var employee = dBContext.Employees.Find(employeeId);
                if (employee == null) return false;

                dBContext.Employees.Remove(employee);
                var result = dBContext.SaveChanges();
                return result > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> ImportEmployee(List<EmployeeViewModel> employees)
        {
            try
            {
                List<Employee> toDB = new List<Employee>();

                foreach (var employee in employees)
                {
                    Employee emp = new Employee
                    {
                        FullName = employee.FullName,
                        Department = employee.Department,
                        DateOfBirth = employee.DateOfBirth,
                        Age = employee.Age,
                        PhoneNumber = employee.PhoneNumber,
                    };
                    toDB.Add(emp);
                }
                await dBContext.BulkInsertAsync(toDB);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public async Task<Byte[]> ExporttoExcel()
        {
            var datas = await GetAllEmployees();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Employee");

                worksheet.Cell(1, 1).Value = "Employee Id";
                worksheet.Cell(1, 2).Value = "Full Name";
                worksheet.Cell(1, 3).Value = "Department";
                worksheet.Cell(1, 4).Value = "Date of birth";
                worksheet.Cell(1, 5).Value = "Age";
                worksheet.Cell(1, 6).Value = "Phone Number";

                for (int i = 0; i < datas.Count; i++)
                { 
                    worksheet.Cell(i + 2, 1).Value=datas[i].EmployeeIdView;
                    worksheet.Cell(i + 2, 2).Value=datas[i].FullName;
                    worksheet.Cell(i + 2, 3).Value=datas[i].Department;
                    worksheet.Cell(i + 2, 4).Value=datas[i].DateOfBirth;
                    worksheet.Cell(i + 2, 5).Value=datas[i].Age;
                    worksheet.Cell(i + 2, 6).Value=datas[i].PhoneNumber;
                }
                using (var stream = new MemoryStream())
                { 
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
