using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Customers
{
    /// <filename>EditCustomerViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014</date>
    /// <summary>Classe de type ViewModel, qui gère la fenêtre de modification d'un client</summary>
    class EditCustomerViewModel : BaseViewModel
    {
        public Contact customer { get; set; }
        public Dictionary<string, string> civilities { get { return Dictionaries.civilities; } set { } }
        public String civility { get; set; }
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
        public ICommand cmdEdit { get; set; }
        public Action CloseActionEdit { get; set; }


        public Validation validFirstname { get; set; }
        public Validation validLastname { get; set; }

        /// <summary>
        /// Initialise les valeur à binder et lie la commande d'édition à la commande
        /// </summary>
        /// <param name="customer">Le client à modifier</param>
        public EditCustomerViewModel(Contact customer)
        {
            this.customer = customer;
            this.civility = customer.civility;
            this.firstname = customer.firstname;
            this.lastname = customer.lastname;
            this.street = customer.street;
            this.city = customer.city;
            this.zip = customer.zip;
            this.country = customer.country;

            foreach (Phone p in this.customer.phones)
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

        /// <summary>
        ///     Verification des champs saisis dans le formulaire et modification du client
        /// </summary>
        /// <param name="obj"></param>
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
                this.customer.firstname = this.firstname;
                this.customer.lastname = this.lastname;
                this.customer.street = this.street;
                this.customer.city = this.city;
                this.customer.zip = this.zip;
                this.customer.country = this.country;
                this.customer.civility = this.civility;

                // Enregistre dans la base
                db.SaveChanges();

                this.CloseActionEdit();   
            }
        }
    }
}
