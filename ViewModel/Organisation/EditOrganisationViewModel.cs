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
        public string phonePrivate { get; set; }
        public string phonePro { get; set; }
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

                if (!this.street.Equals(""))
                    currentOrganisation.street = this.street;

                if (!this.city.Equals(""))
                    currentOrganisation.city = this.city;

                if (!this.zip.Equals(""))
                    currentOrganisation.zip = this.zip;

                if (!this.country.Equals(""))
                    currentOrganisation.country = this.country;

                // Enregistre dans la base
                db.SaveChanges();

                this.CloseActionFormEdit();
            }
        }
    }
}
