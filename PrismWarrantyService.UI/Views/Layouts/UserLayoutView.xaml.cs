using System.Security.Permissions;
using System.Windows.Controls;

namespace PrismWarrantyService.UI.Views.Layouts
{
    [PrincipalPermission(SecurityAction.Demand, Role = "Пользователь")]
    public partial class UserLayoutView : UserControl
    {
        public UserLayoutView()
        {
            InitializeComponent();
        }
    }
}
