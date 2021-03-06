using EmployeeBenefits.MVC.Services.Contracts;
using EmployeeBenefits.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBenefits.MVC.Controllers
{
    [Authorize]
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeBenefitsViewModel>> GetEmployeeBenefitsByEmployeeIdAsync(int employeeID)
        {
            var benefits = await _benefitsService.GetEmployeeBenefitsByEmployeeIdAsync(employeeID);
            if (benefits == null) return NotFound("Employee Not Found!");
            return benefits;
        }
    }
}
