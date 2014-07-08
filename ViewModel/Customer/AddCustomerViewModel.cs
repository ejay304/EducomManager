using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public string phonePrivate { get; set; }

        public string phonePro { get; set; }

        public string email { get; set; }

        public Validation validFirstname { get; set; }

        public Validation validLastname { get; set; }

        public ICommand cmdAdd { get; set; }

        public Action CloseActionFormAdd { get; set; }

        public AddCustomerViewModel()
        {
            this.cmdAdd = new RelayCommand<object>(actAdd);
        }

        public void actAdd(object obj)
        {

            bool error = false;
            this.validFirstname = new Validation();
            this.validLastname = new Validation();

            contact customer = new contact();
            customer.civility = "m";

            // Validation prénom
            if (!this.firstname.Equals(""))
            {
                customer.firstname = this.firstname;
                this.validFirstname.message = "Valide";
                this.validFirstname.valid = true;
                NotifyPropertyChanged("validFirstname");
            }
            else
            {
                this.validFirstname.message = "Champ requis";
                this.validFirstname.valid = false;
                NotifyPropertyChanged("validFirstname");
                error = true;
            }

            // Validation nom
            if (!this.lastname.Equals(""))
            {
                customer.lastname = this.lastname;
                this.validLastname.message = "Valide";
                this.validLastname.valid = true;
                NotifyPropertyChanged("validLastname");
            }
            else
            {
                this.validLastname.message = "Champ requis";
                this.validLastname.valid = false;
                NotifyPropertyChanged("validLastname");
                error = true;
            }

            if (!this.street.Equals(""))
                customer.street = this.street;

            if (!this.city.Equals(""))
                customer.city = this.city;

            if (!this.zip.Equals(""))
                customer.zip = this.zip;

            if (!this.country.Equals(""))
                customer.country = this.country;

            if (!this.phonePrivate.Equals("")) 
            {
                phone phone = new phone();
                phone.number = this.phonePrivate;
                phone.description = "private";
                phone.main = true;
                customer.phones.Add(phone);
            }

            if (!this.phonePro.Equals(""))
            {
                phone phone = new phone();
                phone.number = this.phonePro;
                phone.description = "pro";
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

            if (!error)
            {
                customer.add_date = DateTime.Now;

                // Enregistre dans la base
                db.contacts.Add(customer);
                db.SaveChanges();

                mediator.NotifyViewModel(Helper.Event.ADD_CUSTOMER, customer);

                this.CloseActionFormAdd();
            }
        }
    }
}
