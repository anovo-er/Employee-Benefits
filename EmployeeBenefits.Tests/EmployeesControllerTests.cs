using EmployeeBenefits.MVC.Controllers;
using EmployeeBenefits.MVC.Services.Contracts;
using EmployeeBenefits.MVC.ViewModels;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace EmployeeBenefits.Test
{
    public class EmployeesControllerTests
    {
        private IEmployeeService _employeeService;
        private ILogger<EmployeeController> _logger;
        private EmployeeController _controller;

        [SetUp]
        public void Setup()
        {
            _employeeService = A.Fake<IEmployeeService>();
            _logger = A.Fake<ILogger<EmployeeController>>();
            _controller = new EmployeeController(_logger, _employeeService);
        }

        [Test]
        public async Task CreateEmployeeAsync_Test()
        {
            //Arrange
            var urlHelper = A.Fake<IUrlHelper>();
            var fakeEmployeeOne = A.Dummy<EmployeeViewModel>();
            var fakeEmployeeTwo = A.Dummy<EmployeeViewModel>();

            A.CallTo(() => urlHelper.Link(null, null)).Returns("Resource Url");
            var serviceResponeOne = Tuple.Create<bool, string, int?>(true, null, 1).ToValueTuple();
            var serviceResponeTwo = Tuple.Create<bool, string, int?>(false, "Operation Error!", null).ToValueTuple();
            A.CallTo(()=> _employeeService.CreateEmployeeAsync(fakeEmployeeOne)).Returns(Task.FromResult(serviceResponeOne));
            A.CallTo(()=> _employeeService.CreateEmployeeAsync(fakeEmployeeTwo)).Returns(Task.FromResult(serviceResponeTwo));

            _controller.Url = urlHelper;

            //Act
            var okResponse = await _controller.CreateEmployeeAsync(fakeEmployeeOne);
            var operationErrorResponse = await _controller.CreateEmployeeAsync(fakeEmployeeTwo);

            //Assert
            Assert.IsInstanceOf<CreatedResult>(okResponse);
            Assert.IsInstanceOf<BadRequestObjectResult>(operationErrorResponse);
        }

        [Test]
        public async Task GetEmployeeByIdAsync_Test()
        {
            var fakeEmployee = A.Dummy<EmployeeViewModel>();

            A.CallTo(() => _employeeService.GetEmployeeByIdAsync(1)).Returns(Task.FromResult<EmployeeViewModel>(null));
            A.CallTo(() => _employeeService.GetEmployeeByIdAsync(2)).Returns(Task.FromResult(fakeEmployee));

            //Act
            var okResponse = await _controller.GetEmployeeByIdAsync(2);
            var notFoundResponse = await _controller.GetEmployeeByIdAsync(1);

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(okResponse);
            Assert.IsInstanceOf<NotFoundObjectResult>(notFoundResponse);
        }
    }
}
