using System.Threading.Tasks;
using PrismWarrantyService.Domain.Entities;

namespace PrismWarrantyService.UI.Services.Authentification.Abstract
{
    public interface IAuthenticationService
    {
        Task<Employee> AuthenticateEmployeeAsync(string name, string clearTextpassword);
        string CalculateHash(string clearTextPassword, string salt);
    }
}
