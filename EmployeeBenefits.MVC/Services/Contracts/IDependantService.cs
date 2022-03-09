using EmployeeBenefits.MVC.ViewModels;

namespace EmployeeBenefits.MVC.Services.Contracts
{
    public interface IDependantService
    {
        Task<(bool succeded, string ErrorMessage, int? DependantId)> CreateDependantAsync(DependantViewModel dependant);
        Task<(bool succeded, string ErrorMessage, int? DependantId)> UpdateDependantAsync(int dependantId);
        Task<(bool succeded, string ErrorMessage, int? DependantId)> DeleteDependantAsync(int dependantId);
        Task<DependantViewModel> GetDependantByIdAsync(int dependantId);
    }
}
