using EmployeeBenefits.MVC.Data;
using EmployeeBenefits.MVC.Services.Contracts;
using EmployeeBenefits.MVC.ViewModels;
using EmployeeBenefits.MVC.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace EmployeeBenefits.MVC.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly AutoMapper.IConfigurationProvider _mappingConfig;

        public EmployeeService(ApplicationDbContext dbContext, AutoMapper.IConfigurationProvider mappingConfig)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
            _mappingConfig = mappingConfig;
        }

        public async Task<(bool succeded, string ErrorMessage, int? employeeId)> CreateEmployeeAsync(EmployeeViewModel employeeViewModel)
        {
            var mapper = _mappingConfig.CreateMapper();
            var newEmployee = mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
             _dbContext.Employees.Add(newEmployee);

            try
            {
                await _dbContext.SaveChangesAsync();
                return (true, "Created", newEmployee.Id);
            }
            catch (Exception e)
            {
                return (false, $"Operation Failed. {e.Message}", null);
            }
        }

        public async Task<EmployeeViewModel> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await _dbContext.Employees.FindAsync(employeeId);
            if (employee == null) return null;

            var mapper = _mappingConfig.CreateMapper();
            return mapper.Map(employee, new EmployeeViewModel());
        }

        public Task<(bool succeded, string ErrorMessage, int? employeeId)> UpdateEmployeeAsync(int employeeId)
        {
            //Not implemented for simplicity.
            throw new NotImplementedException();
        }
        
        public Task<(bool succeded, string ErrorMessage, int? employeeId)> DeleteEmployeeAsync(int employeeId)
        {
            //Not implemented for simplicity.
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetEmployeesAsync()
        {
            IQueryable<Employee> employees = _dbContext.Employees;
            return await employees.ProjectTo<EmployeeViewModel>(_mappingConfig).ToArrayAsync();
        }
    }
}
