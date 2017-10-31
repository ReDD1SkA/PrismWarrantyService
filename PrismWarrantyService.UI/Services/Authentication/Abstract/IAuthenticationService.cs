using PrismWarrantyService.Domain.Entities;

namespace PrismWarrantyService.UI.Services.Authentification.Abstract
{
    public interface IAuthenticationService
    {
        Employee AuthenticateEmployee(string name, string clearTextpassword);
    }
}
