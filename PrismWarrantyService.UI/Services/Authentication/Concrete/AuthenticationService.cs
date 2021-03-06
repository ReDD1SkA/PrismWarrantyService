﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Services.Authentification.Abstract;

namespace PrismWarrantyService.UI.Services.Authentification.Concrete
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields

        private readonly IRepository _repository;

        #endregion

        #region Constructors and finalizers

        public AuthenticationService(IRepository repo)
        {
            _repository = repo;
        }

        #endregion

        #region Methods

        public async Task<Employee> AuthenticateEmployeeAsync(string login, string clearTextPassword)
        {
            var employee = await _repository
                .GetEmployeeByLoginAsync(login);

            if (employee == null || CalculateHash(clearTextPassword, employee.Login) != employee.HashedPassword)
                throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");

            return employee;
        }

        public string CalculateHash(string clearTextPassword, string salt)
        {
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            return Convert.ToBase64String(hash);
        }

        #endregion
    }
}