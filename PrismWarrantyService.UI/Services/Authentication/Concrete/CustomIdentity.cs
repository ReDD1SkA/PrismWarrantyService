using PrismWarrantyService.Domain.Entities;
using System.Security.Principal;

namespace PrismWarrantyService.UI.Services.Authentification.Concrete
{
    public class CustomIdentity : IIdentity
    {
        #region Constructors and finalizers

        public CustomIdentity(string name, Role role)
        {
            Name = name;
            Role = role;
        }

        #endregion

        #region Properties

        public string Name { get; }
        public Role Role { get; }

        public string AuthenticationType => "Custom authentication";

        public bool IsAuthenticated => Role != null;

        #endregion
    }
}
