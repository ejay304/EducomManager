using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Customer
{
    class EditCustomerViewModel : BaseViewModel
    {
        public contact currentCustomer { get; set; }

        public String firstName { get; set; }

        public String lastName { get; set; }

        public ICommand cmdEdit { get; set; }

        public Action CloseActionFormAdd { get; set; }

        public EditCustomerViewModel(contact customer)
        {
            this.currentCustomer = customer;

            this.firstName = customer.firstname;
            this.lastName = customer.lastname;

            this.cmdEdit = new RelayCommand<object>(actEdit);
        }

        public void actEdit(object obj)
        {
            this.currentCustomer.firstname = this.firstName;
            this.currentCustomer.lastname = this.lastName;

            db.SaveChanges();

            this.CloseActionFormAdd();
        }
    }
}
