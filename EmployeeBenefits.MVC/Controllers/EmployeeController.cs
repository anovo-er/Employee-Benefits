using EmployeeBenefits.MVC.Infrastructure;
using EmployeeBenefits.MVC.Services.Contracts;
using EmployeeBenefits.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBenefits.MVC.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(
            ILogger<EmployeeController> logger, 
            IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpPost(Name = nameof(CreateEmployeeAsync))]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public async Task<ActionResult> CreateEmployeeAsync([FromBody] EmployeeViewModel employeeViewModel) {
            
            if(!ModelState.IsValid) return BadRequest(new ApiError(ModelState));
            
            var (succeded, message, newEmployeeId) = await _employeeService.CreateEmployeeAsync(employeeViewModel);

            if (succeded)
                return Created(Url.Link(nameof(GetEmployeeByIdAsync), new { employeeId = newEmployeeId }), newEmployeeId);

            return BadRequest(new ApiError { Detail = message, Message = "Could not create resource"});
        }

        [HttpGet("{employeeId}", Name = nameof(GetEmployeeByIdAsync))]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetEmployeeByIdAsync(int employeeID)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(employeeID);
            if (employee == null) return NotFound("Employee Not Found!");
            return Ok(employee);
        }

        [HttpGet(Name = nameof(GetEmployeesAsync))]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetEmployeesAsync()
        { 
            var result = await _employeeService.GetEmployeesAsync();
            return Ok(result);
        }
    }
}
