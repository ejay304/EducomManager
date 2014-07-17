using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisation
{
    class EditOrganisationViewModel :BaseViewModel
    {
        public organisation currentOrganisation { get; set; }
        public string name { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string email { get; set; }

        public Validation validName { get; set; }

        public ICommand cmdEdit { get; set; }

        public Action CloseActionFormEdit { get; set; }

        public EditOrganisationViewModel(organisation organisation) {

            this.currentOrganisation = organisation;
            this.name = organisation.name;
            this.street = organisation.street;
            this.city = organisation.city;
            this.zip = organisation.zip;
            this.country = organisation.country;

            if (organisation.phone != null)
                this.phone = organisation.phone.number;
            else
                this.phone = "";

            if(organisation.email != null)
                this.email = organisation.email.email1;
            else
                this.email = "";

            this.cmdEdit = new RelayCommand<object>(actEdit);        
        }

        public void actEdit(object obj)
        {

            bool error = false;
            this.validName = new Validation();

            // Validation nom de l'organisation
            if (!this.name.Equals(""))
            {
                this.validName.message = "Valide";
                this.validName.valid = true;
                NotifyPropertyChanged("validFirstname");
            }
            else
            {
                this.validName.message = "Champ requis";
                this.validName.valid = false;
                NotifyPropertyChanged("validFirstname");
                error = true;
            }

            if (!error)
            {
                currentOrganisation.name = this.name;
                currentOrganisation.street = this.street;
                currentOrganisation.city = this.city;
                currentOrganisation.zip = this.zip;
                currentOrganisation.country = this.country;

                // Numéro de téléphone
                if (!this.phone.Equals(""))
                {
                    if (currentOrganisation.phone != null)
                        currentOrganisation.phone.number = this.phone;
                    else
                    {
                        phone phone = new phone();
                        phone.number = this.phone;
                        phone.description = "main";
                        phone.main = true;

                        currentOrganisation.phone = phone;
                    }
                }

                // Adresse email
                if (!this.email.Equals(""))
                {
                    if (currentOrganisation.email != null)
                        currentOrganisation.email.email1 = this.email;
                    else
                    {
                        email email = new email();
                        email.email1 = this.email;
                        email.main = true;

                        currentOrganisation.email = email;
                    }
                }

                // Enregistre dans la base
                db.SaveChanges();

                this.CloseActionFormEdit();
            }
        }
    }
}
