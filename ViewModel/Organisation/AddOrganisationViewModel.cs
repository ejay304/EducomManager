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
    class AddOrganisationViewModel : BaseViewModel
    {

        public string name { get; set; }

        public string street { get; set; }

        public string city { get; set; }

        public string zip { get; set; }

        public string country { get; set; }

        public string phone { get; set; }

        public string email { get; set; }

        public Validation validName { get; set; }

        public ICommand cmdAdd { get; set; }

        public Action CloseActionFormAdd { get; set; }

        public AddOrganisationViewModel() 
        {
            this.cmdAdd = new RelayCommand<object>(actAdd);
        }

        public void actAdd(object obj)
        {

            bool error = false;
            this.validName = new Validation();

            organisation organisation = new organisation();

            // Validation nom de l'organisation
            if (!this.name.Equals(""))
            {
                organisation.name = this.name;
                this.validName.message = "Valide";
                this.validName.valid = true;
                NotifyPropertyChanged("validName");
            }
            else
            {
                this.validName.message = "Champ requis";
                this.validName.valid = false;
                NotifyPropertyChanged("validName");
                error = true;
            }

            if (!this.street.Equals(""))
                organisation.street = this.street;

            if (!this.city.Equals(""))
                organisation.city = this.city;

            if (!this.zip.Equals(""))
                organisation.zip = this.zip;

            if (!this.country.Equals(""))
                organisation.country = this.country;

            if (!this.phone.Equals(""))
            {
                phone phone = new phone();
                phone.number = this.phone;
                phone.description = "main";
                phone.main = true;

                //organisation.phone = phone;
            }

            if (!this.email.Equals(""))
            {
                email email = new email();
                email.email1 = this.email;
                email.main = true;

                //organisation.email = email;
            }

            if (!error)
            {
                // Enregistre dans la base
                db.organisations.Add(organisation);
                db.SaveChanges();

                mediator.NotifyViewModel(Helper.Event.ADD_ORGANISATION, organisation);

                this.CloseActionFormAdd();
            }
        }
    }
}
