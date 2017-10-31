using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Moq;
using System.Collections.Generic;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.UI.Services.Authentification.Abstract;
using PrismWarrantyService.UI.Services.Authentification.Concrete;

namespace PrismWarrantyService.Tests
{
    [TestClass]
    public class AuthentificationTest
    {
        [TestMethod]
        public void AuthentificateRealEmployee()
        {
            // arrange
            var employee = new Employee()
            {
                Login = "irva",
                HashedPassword = "Nu7kMuYCZzkpnUUxujf4gm1NVYSFc0qCK/vDWS6xfNk="
            };
            var employeeList = new List<Employee> { employee };

            var mock = new Mock<IRepository>();
            mock.Setup(m => m.Employees).Returns(employeeList.AsQueryable);

            IAuthenticationService service = new AuthenticationService(mock.Object);

            // act
            var candidate = service.AuthenticateEmployee("irva", "adczq2dac");

            // assert
            Assert.AreEqual(employee, candidate);
        }
    }
}
