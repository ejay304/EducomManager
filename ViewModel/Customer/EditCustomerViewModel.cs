using PrototypeEDUCOM.Helper;
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

        public int civilityIndex { get; set; }
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

        public ICommand cmdEdit { get; set; }

        public Action CloseActionFormAdd { get; set; }

        public EditCustomerViewModel(contact customer)
        {
            this.currentCustomer = customer;

            this.firstname = customer.firstname;
            this.lastname = customer.lastname;
            this.street = customer.street;
            this.city = customer.city;
            this.zip = customer.zip;
            this.country = customer.country;


            this.cmdEdit = new RelayCommand<object>(actEdit);
        }

        public void actEdit(object obj)
        {

            bool error = false;
            this.validFirstname = new Validation();
            this.validLastname = new Validation();

            // Validation prénom
            if (!this.firstname.Equals(""))
            {
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

            if (!error)
            {

                currentCustomer.firstname = this.firstname;
                currentCustomer.lastname = this.lastname;

                 if (!this.street.Equals(""))
                    currentCustomer.street = this.street;

                if (!this.city.Equals(""))
                    currentCustomer.city = this.city;

                if (!this.zip.Equals(""))
                    currentCustomer.zip = this.zip;

                if (!this.country.Equals(""))
                    currentCustomer.country = this.country;

                // Enregistre dans la base
                db.SaveChanges();

                this.CloseActionFormAdd();
                
            }
        }
    }
}
