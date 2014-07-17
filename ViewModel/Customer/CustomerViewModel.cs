using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Model;
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
            customerTabs.Add(new Tab("Liste", mediator.openListCustomerView(), null, null));
        }

        private void actCloseTab(Tab tab)
        {
            customerTabs.Remove(tab);
            NotifyPropertyChanged("customerTabs");
        }

        public void actAddTab(contact customer, UserControl view)
        {
            Tab tab = new Tab(customer.lastname, view,customer, null);

            this.customerTabs.Add(tab);
            this.selectedTab = tab;

            mediator.Register(Helper.Event.DELETE_CUSTOMER, this);
        }

        public override void Update(string eventName, Object item)
        {
            switch (eventName)
            {
                case Helper.Event.DELETE_CUSTOMER:

                    for(int i = 0 ; i < customerTabs.Count() ; i++) {
                        if (customerTabs[i].entity == item)
                        {
                            customerTabs.Remove(customerTabs[i]);
                            i--;
                        }
                    }
                    
                    NotifyPropertyChanged("customerTabs");
                    NotifyPropertyChanged("selectedTab");
                    break;
            }
        }
    }
}
