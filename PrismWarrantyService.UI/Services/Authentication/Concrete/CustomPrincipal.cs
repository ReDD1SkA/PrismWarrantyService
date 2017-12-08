using System.Linq;
using System.Security.Principal;

namespace PrismWarrantyService.UI.Services.Authentification.Concrete
{
    public class CustomPrincipal : IPrincipal
    {
        #region Fields

        private CustomIdentity _identity;

        #endregion

        #region Properties

        public CustomIdentity Identity
        {
            get => _identity ?? new AnonymousIdentity();
            set => _identity = value;
        }

        IIdentity IPrincipal.Identity => Identity;

        #endregion

        #region Methods

        public bool IsInRole(string role)
        {
            return _identity.Role.Name.Equals(role);
        }

        #endregion
    }
}
