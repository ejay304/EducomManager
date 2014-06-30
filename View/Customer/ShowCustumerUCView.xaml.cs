using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrototypeEDUCOM.View.Customer
{
    /// <summary>
    /// Logique d'interaction pour ShowCustumerUCView.xaml
    /// </summary>
    public partial class ShowCustumerUCView : UserControl
    {
        public ShowCustumerUCView()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.Customer.ShowCustomerViewModel();
        }
    }
}
