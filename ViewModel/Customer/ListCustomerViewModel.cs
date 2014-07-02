using PrototypeEDUCOM.Model;
using PrototypeEDUCOM.View.Customer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Customer
{
    class ListCustomerViewModel : BaseViewModel
    {

        private ViewModel.Customer.CustomerViewModel parentVM;
        public ObservableCollection<contact> customers { get; set; }

        public int nbrCustomer 
        { 
            get { return this.customers.Count; } 
        }

        public ICommand cmdViewDetail { get; set; }

        public ICommand cmdAdd { get; set; }
        public Action CloseActionFormEdit { get; set; }

        public ListCustomerViewModel(ViewModel.Customer.CustomerViewModel parentVM) : base()
        {
            this.parentVM = parentVM;
            this.customers = new ObservableCollection<contact>(db.contacts.ToList());
            this.cmdViewDetail = new RelayCommand<contact>(actViewDetail);
            this.cmdAdd = new RelayCommand<object>(actAdd);           
        }

        private void actAdd(object obj)
        {
            AddCustomerViewModel addCustomerViewModel = new AddCustomerViewModel(this);
            AddCustomerView addCustomerView = new AddCustomerView();

            addCustomerView.DataContext = addCustomerViewModel;
            addCustomerViewModel.CloseActionFormAdd = new Action(() => addCustomerView.Close());

            addCustomerView.Show(); 
        }

        public void actViewDetail(contact customer)
        {
            Tab tab = new Tab(customer.lastname, new View.Customer.ShowCustomerUCView(customer), null);

            parentVM.customerTabs.Add(tab);
            parentVM.selectedTab = tab;
        }
    }
}
