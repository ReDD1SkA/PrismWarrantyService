using System.Linq;
using System.Security.Principal;

namespace PrismWarrantyService.UI.Services.Authentification.Concrete
{
    public class CustomPrincipal : IPrincipal
    {
        #region Fields

        private CustomIdentity identity;

        #endregion

        #region Properties

        public CustomIdentity Identity
        {
            get { return identity ?? new AnonymousIdentity(); }
            set { identity = value; }
        }

        IIdentity IPrincipal.Identity
        {
            get { return Identity; }
        }

        #endregion

        #region Methods

        public bool IsInRole(string role)
        {
            return identity.Role.Name.Equals(role);
        }

        #endregion
    }
}
