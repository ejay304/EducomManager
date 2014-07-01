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

        public ICommand cmdViewDetail { get; set; }

        public ICommand cmdDelete { get; set; }

        public ICommand cmdAdd { get; set; }

        public ICommand cmdEdit { get; set; }

        public Action CloseActionFormEdit { get; set; }

        public ListCustomerViewModel(ViewModel.Customer.CustomerViewModel parentVM) : base()
        {
            this.parentVM = parentVM;
            this.customers = new ObservableCollection<contact>(db.contacts.ToList());
            this.cmdViewDetail = new RelayCommand<contact>(actViewDetail);
            this.cmdDelete = new RelayCommand<contact>(actDelete);
            this.cmdAdd = new RelayCommand<object>(actAdd);           
            this.cmdEdit = new RelayCommand<contact>(actEdit);
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

        public void actDelete(contact customer)
        {
            customers.Remove(customer);
            db.contacts.Remove(customer);
            db.SaveChanges();
            NotifyPropertyChanged("customers");
        }

        public void actEdit(contact contact)
        {
            EditCustomerViewModel editCustomerViewModel = new EditCustomerViewModel(contact);
            EditCustomerView editCustomerView = new EditCustomerView();

            editCustomerView.DataContext = editCustomerViewModel;
            editCustomerViewModel.CloseActionFormAdd = new Action(() => editCustomerView.Close());

            editCustomerView.Show(); 
        }
    }
}
