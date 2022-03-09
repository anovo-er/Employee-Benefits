using EmployeeBenefits.MVC.Services.Contracts;
using EmployeeBenefits.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBenefits.MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenefitsController : ControllerBase
    {
        private readonly ILogger<BenefitsController> _logger;
        private readonly IBenefitsService _benefitsService;

        public BenefitsController(
            ILogger<BenefitsController> logger,
            IBenefitsService benefitsService)
        {
            _logger = logger;
            _benefitsService = benefitsService;
        }

        [HttpGet("{employeeId}", Name = nameof(GetEmployeeBenefitsByEmployeeIdAsync))]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<EmployeeBenefitsViewModel>> GetEmployeeBenefitsByEmployeeIdAsync(int employeeID)
        {
            var benefits = await _benefitsService.GetEmployeeBenefitsByEmployeeIdAsync(employeeID);
            if (benefits == null) return NotFound("Employee Not Found!");
            return benefits;
        }
    }
}
