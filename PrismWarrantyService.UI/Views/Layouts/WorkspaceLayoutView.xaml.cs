using System.Security.Permissions;
using System.Windows.Controls;

namespace PrismWarrantyService.UI.Views.Layouts
{
    [PrincipalPermission(SecurityAction.Demand)]
    public partial class WorkspaceLayoutView : UserControl
    {
        public WorkspaceLayoutView()
        {
            InitializeComponent();
        }
    }
}
