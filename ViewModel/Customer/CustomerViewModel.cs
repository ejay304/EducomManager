using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Customer
{
    class CustomerViewModel : BaseViewModel
    {

        private Tab _selectedTab;

        public ICommand cmdCloseTab { get; set; }
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

            this.cmdCloseTab = new RelayCommand<Tab>(actCloseTab);
            customerTabs = new ObservableCollection<Tab>();
            View.Customer.ListCustomerUCView view = new View.Customer.ListCustomerUCView();
            view.DataContext = new ViewModel.Customer.ListCustomerViewModel(this);
            customerTabs.Add(new Tab("Liste", view, null));
        }

        private void actCloseTab(Tab tab)
        {
            customerTabs.Remove(tab);
            NotifyPropertyChanged("customerTabs");
        }
    }
}
