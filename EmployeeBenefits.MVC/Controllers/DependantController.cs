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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateDependantAsync([FromBody] DependantViewModel dependantViewModel) {
            
            if(!ModelState.IsValid) return BadRequest(new ApiError(ModelState));
            
            var (succeded, message, newDependantId) = await _dependatService.CreateDependantAsync(dependantViewModel);

            if (succeded)
                return Created(Url.Link(nameof(GetDependatByIdAsync), new { dependantId = newDependantId }), newDependantId);
            else if (newDependantId.HasValue)
                return NotFound("EmployeeNotFound");

            return StatusCode(StatusCodes.Status500InternalServerError, new ApiError { Message = "Database exception", Detail = message });
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

        [HttpDelete("{dependantId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteDependantAsync(int dependantId) {
            var (succeded, message, dependantIdToRemove) = await _dependatService.DeleteDependantAsync(dependantId);

            if (succeded)
                return Ok();
            else if (dependantIdToRemove.HasValue)
                return StatusCode(StatusCodes.Status500InternalServerError);
            return NotFound(message);           
        }
    }
}
