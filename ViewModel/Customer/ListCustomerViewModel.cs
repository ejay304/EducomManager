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

        public ICommand cmdAddRequest { get; set; }

        public ICommand cmdFormEditRequest { get; set; }

        public ICommand cmdEdit { get; set; }

        public Action CloseActionFormEdit { get; set; }

        public ListCustomerViewModel(ViewModel.Customer.CustomerViewModel parentVM) : base()
        {
            this.parentVM = parentVM;
            this.customers = new ObservableCollection<contact>(db.contacts.ToList());
            this.cmdViewDetail = new RelayCommand<contact>(actViewDetail);
            this.cmdDelete = new RelayCommand<request>(actDelete);
            this.cmdAddRequest = new RelayCommand<object>(actAddRequest);
            this.cmdFormEditRequest = new RelayCommand<request>(actFormEditRequest);
           
            this.cmdEdit = new RelayCommand<request>(actEdit);
        }

        private void actAddRequest(object obj)
        {
            AddCustomerViewModel addCustomerViewModel = new AddCustomerViewModel(this);
            AddCustomerView addCustomerView = new AddCustomerView();

            addCustomerView.DataContext = addCustomerViewModel;
            addCustomerViewModel.CloseActionFormAdd = new Action(() => addCustomerView.Close());

            addCustomerView.Show(); 
        }

        private void actFormEditRequest(request request)
        {

        }

        public void actViewDetail(contact customer)
        {
            Tab tab = new Tab(customer.lastname, new View.Customer.ShowCustumerUCView());

            parentVM.customerTabs.Add(tab);
            parentVM.selectedTab = tab;
        }

        public void actDelete(request request)
        {

        }

        public void actEdit(object obj)
        {

        }
    }
}
