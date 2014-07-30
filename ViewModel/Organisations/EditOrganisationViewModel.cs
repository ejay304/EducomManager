using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisations
{
    class EditOrganisationViewModel :BaseViewModel
    {
        public Organisation organisation { get; set; }
        public string name { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string email { get; set; }

        public Validation validName { get; set; }

        public ICommand cmdEdit { get; set; }

        public Action CloseActionEdit { get; set; }

        public EditOrganisationViewModel(Organisation organisation) {

            this.organisation = organisation;
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
                organisation.name = this.name;
                organisation.street = this.street;
                organisation.city = this.city;
                organisation.zip = this.zip;
                organisation.country = this.country;

                // Numéro de téléphone
                if (!this.phone.Equals(""))
                {
                    if (organisation.phone != null)
                        organisation.phone.number = this.phone;
                    else
                    {
                        Phone phone = new Phone();
                        phone.number = this.phone;
                        phone.description = "main";
                        phone.main = true;

                        organisation.phone = phone;
                    }
                }

                // Adresse email
                if (!this.email.Equals(""))
                {
                    if (organisation.email != null)
                        organisation.email.email1 = this.email;
                    else
                    {
                        Email email = new Email();
                        email.email1 = this.email;
                        email.main = true;

                        organisation.email = email;
                    }
                }

                // Enregistre dans la base
                db.SaveChanges();

                this.CloseActionEdit();
            }
        }
    }
}
