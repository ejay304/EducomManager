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
    /// Logique d'interaction pour CustomerUCView.xaml
    /// </summary>
    public partial class CustomerUCView : UserControl
    {
        public CustomerUCView()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.Customer.CustomerViewModel();
        }
    }
}
