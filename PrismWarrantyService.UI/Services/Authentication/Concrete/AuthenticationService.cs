using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Services.Authentification.Abstract;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PrismWarrantyService.UI.Services.Authentification.Concrete
{
    public class AuthenticationService : IAuthenticationService
    {
        private IRepository repository;

        public AuthenticationService(IRepository repo)
        {
            repository = repo;
        }

        #region Methods
        public Employee AuthenticateEmployee(string login, string clearTextPassword)
        {

            Employee employee = repository.Employees
                .Where(x => x.Login == login)
                .FirstOrDefault();

            if (employee == null)
                throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");
            if (CalculateHash(clearTextPassword, employee.Login) != employee.HashedPassword)
                throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");

            return employee;
        }

        private string CalculateHash(string clearTextPassword, string salt)
        {
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            return Convert.ToBase64String(hash);
        }
        #endregion
    }
}