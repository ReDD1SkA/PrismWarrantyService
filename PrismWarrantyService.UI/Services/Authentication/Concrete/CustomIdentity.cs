using PrismWarrantyService.Domain.Entities;
using System.Security.Principal;

namespace PrismWarrantyService.UI.Services.Authentification.Concrete
{
    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(string name, Role role)
        {
            Name = name;
            Role = role;
        }

        #region Properties
        public string Name { get; private set; }
        public Role Role { get; private set; }

        public string AuthenticationType
        {
            get { return "Custom authentication"; }
        }

        public bool IsAuthenticated
        {
            get { return Role != null ? true : false; }
        }
        #endregion
    }
}
