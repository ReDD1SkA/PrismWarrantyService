using System.Security.Permissions;
using System.Windows.Controls;

namespace PrismWarrantyService.UI.Views.Layouts
{
    [PrincipalPermission(SecurityAction.Demand, Role = "Пользователь")]
    public partial class EmployeeWorkspaceLayoutView : UserControl
    {
        public EmployeeWorkspaceLayoutView()
        {
            InitializeComponent();
        }
    }
}
