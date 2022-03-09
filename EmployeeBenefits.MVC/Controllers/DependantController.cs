using EmployeeBenefits.MVC.Infrastructure;
using EmployeeBenefits.MVC.Services.Contracts;
using EmployeeBenefits.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBenefits.MVC.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DependantController : ControllerBase
    {
        private readonly ILogger<DependantController> _logger;
        private readonly IDependantService _dependatService;

        public DependantController(
            ILogger<DependantController> logger,
            IDependantService dependatService)
        {
            _logger = logger;
            _dependatService = dependatService;
        }

        [HttpPost(Name = nameof(CreateDependantAsync))]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public async Task<ActionResult> CreateDependantAsync([FromBody] DependantViewModel dependantViewModel) {
            
            if(!ModelState.IsValid) return BadRequest(new ApiError(ModelState));
            
            var (succeded, message, newDependantId) = await _dependatService.CreateDependantAsync(dependantViewModel);

            if (succeded)
                return Created(Url.Link(nameof(GetDependatByIdAsync), new { dependantId = newDependantId }), newDependantId);
            else if (newDependantId.HasValue)
                return NotFound("EmployeeNotFound");

            return BadRequest(new ApiError { Detail = message, Message = "Could not create resource"});
        }

        [HttpGet("{dependantId}", Name = nameof(GetDependatByIdAsync))]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetDependatByIdAsync(int dependantId)
        {
            var dependant = await _dependatService.GetDependantByIdAsync(dependantId);
            if (dependant == null) return NotFound("Dependat Not Found!");
            return Ok(dependant);
        }
    }
}
