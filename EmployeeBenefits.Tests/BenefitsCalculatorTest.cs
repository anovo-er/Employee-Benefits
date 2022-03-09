using EmployeeBenefits.MVC.Core;
using NUnit.Framework;

namespace EmployeeBenefits.Test
{
    public class BenefitsCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void EmployeeGetsDiscount()
        {
            var result = BenefitsCalculator.EmployeeBenefitsCost("Albert");
            Assert.Greater(Constants.EmployeesBaseBenefitsCost, result);
        }

        [Test]
        public void EmployeeDoesNotGetsDiscount()
        {
            var result = BenefitsCalculator.EmployeeBenefitsCost("Robert");
            Assert.AreEqual(result, Constants.EmployeesBaseBenefitsCost);
        }

        [Test]
        public void DependatGetsDiscount()
        {
            var result = BenefitsCalculator.DependantBenefitsCost("Alex");
            Assert.Greater(Constants.DependantsBaseBenefitsCost, result);
        }

        [Test]
        public void DependantDoesNotGetsDiscount()
        {
            var result = BenefitsCalculator.DependantBenefitsCost("Lorex");
            Assert.AreEqual(result, Constants.DependantsBaseBenefitsCost);
        }

        [Test]
        public void EmployeeSalaryAfterDeductions()
        {
            var employeeBenefitsCost = BenefitsCalculator.EmployeeBenefitsCost("John");
            var dependatBenefitsCost = BenefitsCalculator.DependantBenefitsCost("Alex");
            var totalCost = employeeBenefitsCost + dependatBenefitsCost;

            var result = BenefitsCalculator.EmployeePaycheckAfterDeductions(totalCost);
            Assert.AreEqual(decimal.Round(result, 2), 1944.23);
        }
    }
}