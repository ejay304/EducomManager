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

        public Action CloseActionAdd { get; set; }

        public AddOrganisationViewModel() 
        {
            this.cmdAdd = new RelayCommand<object>(actAdd);
        }

        public void actAdd(object obj)
        {

            bool error = false;
            this.validName = new Validation();

            Organisation organisation = new Organisation();

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
                Phone phone = new Phone();
                phone.number = this.phone;
                phone.description = "main";
                phone.main = true;

                organisation.phone = phone;
            }

            if (!this.email.Equals(""))
            {
                Email email = new Email();
                email.email1 = this.email;
                email.main = true;

                organisation.email = email;
            }

            if (!error)
            {
                // Enregistre dans la base
                db.organisations.Add(organisation);
                db.SaveChanges();

                mediator.NotifyViewModel(Helper.Event.ADD_ORGANISATION, organisation);

                this.CloseActionAdd();
            }
        }
    }
}
