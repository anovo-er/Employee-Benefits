using EmployeeBenefits.MVC.Data;
using EmployeeBenefits.MVC.Models;
using EmployeeBenefits.MVC.Services.Contracts;
using EmployeeBenefits.MVC.ViewModels;

namespace EmployeeBenefits.MVC.Services
{
    public class DependantService : IDependantService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<DependantService> _logger;
        private readonly AutoMapper.IConfigurationProvider _mappingConfig;

        public DependantService(
            ApplicationDbContext dbContext,
            ILogger<DependantService> logger, 
            AutoMapper.IConfigurationProvider mappingConfig)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
            _logger = logger;
            _mappingConfig = mappingConfig;
        }

        public async Task<(bool succeded, string ErrorMessage, int? DependantId)> CreateDependantAsync(DependantViewModel dependantViewModel)
        {
            var employee = await _dbContext.Employees.FindAsync(dependantViewModel.EmployeeId);
            if (employee == null)
                return (false, "Employee Not Found!", dependantViewModel.EmployeeId);

            var mapper = _mappingConfig.CreateMapper();
            var newDependat = mapper.Map<DependantViewModel, Dependant>(dependantViewModel);
            newDependat.Employee = employee;
            _dbContext.Dependants.Add(newDependat);

            try
            {
                await _dbContext.SaveChangesAsync();
                return (true, "Created", newDependat.Id);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Db Error");
                return (false, $"Operation Failed. {e.Message}", null);
            }
        }

        public Task<(bool succeded, string ErrorMessage, int? DependantId)> DeleteDependantAsync(int dependantId)
        {
            throw new NotImplementedException();
        }

        public async Task<DependantViewModel> GetDependantByIdAsync(int dependantId)
        {
            var dependant = await _dbContext.Dependants.FindAsync(dependantId);
            if (dependant == null) return null;

            var mapper = _mappingConfig.CreateMapper();
            return mapper.Map(dependant, new DependantViewModel());
        }

        public Task<(bool succeded, string ErrorMessage, int? DependantId)> UpdateDependantAsync(int dependantId)
        {
            throw new NotImplementedException();
        }
    }
}
