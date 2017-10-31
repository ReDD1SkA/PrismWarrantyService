using System.Security.Permissions;
using System.Windows;

namespace PrismWarrantyService.UI.Views
{
    //[PrincipalPermission(SecurityAction.Demand)]
    public partial class OrderListView : Window
    {
        public OrderListView()
        {
            InitializeComponent();
        }
    }
}
