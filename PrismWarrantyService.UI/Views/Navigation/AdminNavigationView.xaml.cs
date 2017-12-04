using System.Security.Permissions;
using System.Windows.Controls;

namespace PrismWarrantyService.UI.Views.Navigation
{
    [PrincipalPermission(SecurityAction.Demand, Role = "Администратор")]
    public partial class AdminNavigationView : UserControl
    {
        public AdminNavigationView()
        {
            InitializeComponent();
        }
    }
}
