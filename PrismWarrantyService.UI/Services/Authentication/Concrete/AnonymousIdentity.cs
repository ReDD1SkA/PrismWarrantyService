namespace PrismWarrantyService.UI.Services.Authentification.Concrete
{
    public class AnonymousIdentity : CustomIdentity
    {
        public AnonymousIdentity() : base(string.Empty, null) { }
    }
}