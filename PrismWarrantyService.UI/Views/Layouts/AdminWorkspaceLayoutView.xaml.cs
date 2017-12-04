using System.Security.Permissions;
using System.Windows.Controls;

namespace PrismWarrantyService.UI.Views.Layouts
{
    [PrincipalPermission(SecurityAction.Demand, Role = "Администратор")]
    public partial class AdminWorkspaceLayoutView : UserControl
    {
        public AdminWorkspaceLayoutView()
        {
            InitializeComponent();
        }
    }
}
