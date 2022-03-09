using EmployeeBenefits.MVC.ViewModels;

namespace EmployeeBenefits.MVC.Services.Contracts
{
    public interface IBenefitsService
    {
        Task<EmployeeBenefitsViewModel> GetEmployeeBenefitsByEmployeeIdAsync(int employeeId);
    }
}
