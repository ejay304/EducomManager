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

        public string street { get; set; }

        public string city { get; set; }

        public string zip { get; set; }

        public string country { get; set; }

        public ICollection<phone> phones { get; set; }

        public string phone { get; set; }

        public string email { get; set; }

        public ICommand cmdAdd { get; set; }

        public ICommand cmdAddPhone { get; set; }

        public Action CloseActionFormAdd { get; set; }

        public AddCustomerViewModel(ListCustomerViewModel parentVM)
        {
            this.parentVM = parentVM;

            //this.phones = new ICollection<phone>();

            this.cmdAdd = new RelayCommand<object>(actAdd);
            this.cmdAddPhone = new RelayCommand<object>(actAddPhone);
        }

        public void actAddPhone(object obj)
        {
            phone phone = new phone();
            phones.Add(phone);
        }

        public void actAdd(object obj)
        {
            contact customer = new contact();
            customer.civility = "m";
            customer.firstname = this.firstname;
            customer.lastname = this.lastname;

            if (!this.street.Equals(""))
                customer.street = this.street;

            if (!this.city.Equals(""))
                customer.city = this.city;

            if (!this.zip.Equals(""))
                customer.zip = this.zip;

            if (!this.country.Equals(""))
                customer.country = this.country;

            if (!this.phone.Equals("")) 
            {
                phone phone = new phone();
                phone.number = this.phone;
                phone.main = true;
                customer.phones.Add(phone);
            }

            if (!this.email.Equals(""))
            {
                email email = new email();
                email.email1 = this.email;
                email.main = true;
                customer.emails.Add(email);
            }

            customer.add_date = DateTime.Now;

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
