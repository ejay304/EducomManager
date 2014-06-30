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
    /// Logique d'interaction pour ListCustomerUCView.xaml
    /// </summary>
    public partial class ListCustomerUCView : UserControl
    {
        public ListCustomerUCView()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.Customer.ListCustomerViewModel();
        }
    }
}
