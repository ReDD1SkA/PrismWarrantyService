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

        public string Name { get; private set; }
        public Role Role { get; private set; }

        public string AuthenticationType
        {
            get => "Custom authentication";
        }

        public bool IsAuthenticated
        {
            get => Role != null ? true : false;
        }

        #endregion
    }
}
