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

        public ObservableCollection<contact> customers { get; set; }

        public int nbrCustomer 
        { 
            get { return this.customers.Count; } 
        }

        public ICommand cmdViewDetail { get; set; }

        public ICommand cmdAdd { get; set; }
        public Action CloseActionFormEdit { get; set; }

        public ListCustomerViewModel() : base()
        {
            this.customers = new ObservableCollection<contact>(db.contacts.ToList());
            this.cmdViewDetail = new RelayCommand<contact>(actViewDetail);
            this.cmdAdd = new RelayCommand<object>(actAdd);
            mediator.Register(Helper.Event.ADD_CUSTOMER, this);
        }

        private void actAdd(object obj)
        {
            mediator.openAddCustomerView();
        }

        public void actViewDetail(contact customer)
        {
            mediator.openShowCustomerView(customer);
            mediator.Register(Helper.Event.DELETE_CUSTOMER, this);
            mediator.Register(Helper.Event.DELETE_CUSTOMER, mediator.mainTabs["customer"].tabViewModel);
        }

        public override void Update(string eventName, object item)
        {
            switch (eventName)
            {
                case Helper.Event.ADD_CUSTOMER :

                    // Ajoute dans la liste
                    this.customers.Add((contact)item);
                    NotifyPropertyChanged("customers");
                    NotifyPropertyChanged("nbrCustomer");
                    break;
                case Helper.Event.DELETE_CUSTOMER :

                    // Ajoute dans la liste
                    this.customers.Remove((contact)item);
                    NotifyPropertyChanged("customers");
                    NotifyPropertyChanged("nbrCustomer");
                    break;
            }
        }
    }
}
