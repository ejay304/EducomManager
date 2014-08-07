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

namespace PrototypeEDUCOM.ViewModel.Customers
{
    /// <filename>AddCustomerViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère la fenêtre d'ajout de client</summary>
    class AddCustomerViewModel : BaseViewModel
    {

        public Dictionary<string, string> civilities { get { return Dictionaries.civilities; } set { } }
        public string civility { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string particule { get; set; }
        public string language { get; set; }
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
        public Action CloseActionAdd { get; set; }

        /// <summary>
        /// Lie la commandes d'ajout a l'action
        /// </summary>
        public AddCustomerViewModel()
        {
            this.cmdAdd = new RelayCommand<object>(actAdd);
        }

        /// <summary>
        ///     Verification des champs saisis dans le formulaire et ajout du client
        /// </summary>
        public void actAdd(object obj)
        {

            bool error = false;
            this.validFirstname = new Validation();
            this.validLastname = new Validation();

            Contact customer = new Contact();

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

            customer.street = this.street;
            customer.city = this.city;
            customer.zip = this.zip;
            customer.country = this.country;

            if (!this.phonePrivate.Equals("")) 
            {
                Phone phone = new Phone();
                phone.number = this.phonePrivate;
                phone.description = "private";
                phone.main = true;
                customer.phones.Add(phone);
            }

            if (!this.phonePro.Equals(""))
            {
                Phone phone = new Phone();
                phone.number = this.phonePro;
                phone.description = "pro";
                phone.main = true;
                customer.phones.Add(phone);
            }

            if (!this.email.Equals(""))
            {
                Email email = new Email();
                email.email1 = this.email;
                email.main = true;
                customer.emails.Add(email);
            }

            if (!error)
            {
                customer.add_date = DateTime.Now;
                customer.civility = this.civility;
       
                db.contacts.Add(customer);
                db.SaveChanges();

                mediator.NotifyViewModel(Helper.Event.ADD_CUSTOMER, customer);

                this.CloseActionAdd();
            }
        }
    }
}
