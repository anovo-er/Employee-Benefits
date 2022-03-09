using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EmployeeBenefits.MVC.Infrastructure
{
    public class ApiError
    {
        public string Message { get; set; }

        public string Detail { get; set; }

        public ApiError()
        {

        }

        public ApiError(string message)
        {
            Message = message;
        }

        public ApiError(ModelStateDictionary modelState)
        {
            Message = "Invalid Parameters.";
            Detail = modelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage;
        }
    }
}
