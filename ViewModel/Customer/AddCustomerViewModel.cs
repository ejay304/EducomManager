using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Customer
{
    class AddCustomerViewModel : BaseViewModel
    {
        
        private ListCustomerViewModel parentVM;

        public string firstname { get; set; }

        public string lastname { get; set; }

        public ICommand cmdAdd { get; set; }

        public Action CloseActionFormAdd { get; set; }

        public AddCustomerViewModel(ListCustomerViewModel parentVM)
        {
            this.parentVM = parentVM;
            this.cmdAdd = new RelayCommand<object>(actAdd);
        }

        public void actAdd(object obj)
        {
            contact customer = new contact();
            customer.civility = "m";
            customer.firstname = this.firstname;
            customer.lastname = this.lastname;

            // Ajoute dans la liste
            parentVM.customers.Add(customer);
            parentVM.NotifyPropertyChanged("customers");
            parentVM.NotifyPropertyChanged("nbrCustomer");

            // Enregistre dans la base
            db.contacts.Add(customer);
            db.SaveChanges();

            this.CloseActionFormAdd();
        }
    }
}
