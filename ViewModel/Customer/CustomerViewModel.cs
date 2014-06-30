using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeEDUCOM.ViewModel.Customer
{
    class CustomerViewModel : BaseViewModel
    {

        private Tab _selectedTab;

        public ObservableCollection<Tab> customerTabs { get; set; }
        public Tab selectedTab {
            get { return _selectedTab;  }
            set {
                _selectedTab = value;
                NotifyPropertyChanged("selectedTab");
            }
        }

        public CustomerViewModel()
        {
            customerTabs = new ObservableCollection<Tab>();
            View.Customer.ListCustomerUCView view = new View.Customer.ListCustomerUCView();
            view.DataContext = new ViewModel.Customer.ListCustomerViewModel(this);
            customerTabs.Add(new Tab("Liste", view));
        }
    }
}
