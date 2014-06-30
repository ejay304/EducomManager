using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel
{
    class Tab
    {
        public String header { get; set; }

        public UserControl content { get; set; }

        public Tab(String header, UserControl content)
        {
            this.header = header;
            this.content = content;
        }
    }

    class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Tab> tabs { get; set; }

        public MainViewModel()
        {
            tabs = new ObservableCollection<Tab>();
            tabs.Add(new Tab("Clients", new View.Customer.ListCustomerUCView()));
        }
    }
}
