using EmployeeBenefits.MVC.ViewModels;

namespace EmployeeBenefits.MVC.Services.Contracts
{
    public interface IEmployeeService
    {
        Task<(bool succeded, string ErrorMessage, int? employeeId)> CreateEmployeeAsync(EmployeeViewModel employee);
        Task<(bool succeded, string ErrorMessage, int? employeeId)> UpdateEmployeeAsync(int employeeId);
        Task<(bool succeded, string ErrorMessage, int? employeeId)> DeleteEmployeeAsync(int employeeId);
        Task<EmployeeViewModel> GetEmployeeByIdAsync(int employeeId);
        Task<IEnumerable<EmployeeViewModel>> GetEmployeesAsync();
    }
}
