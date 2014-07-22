using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Helper.Enum;
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
        public List<Civility> civilities { get; set; }
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

        public Action CloseActionFormEdit { get; set; }

        public EditCustomerViewModel(contact customer)
        {
            this.currentCustomer = customer;
            this.civilities = Civility.list;
            this.civilityIndex = Civility.indexByValue(customer.civility);
            this.firstname = customer.firstname;
            this.lastname = customer.lastname;
            this.street = customer.street;
            this.city = customer.city;
            this.zip = customer.zip;
            this.country = customer.country;

            foreach (phone p in this.currentCustomer.phones)
            {
                switch (p.description)
                {
                    case "private":
                        this.phonePrivate = p.number;
                        break;
                    case "pro":
                        this.phonePro = p.number;
                        break;
                }
            }

            if(customer.emails.Count != 0)
                this.email = customer.emails.First().email1;

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
                currentCustomer.street = this.street;
                currentCustomer.city = this.city;
                currentCustomer.zip = this.zip;
                currentCustomer.country = this.country;
                currentCustomer.civility = civilities.ElementAt(civilityIndex).getValue();

                // Enregistre dans la base
                db.SaveChanges();

                this.CloseActionFormEdit();
                
            }
        }
    }
}
