using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisations.Programs.Campuses
{
    /// <filename>EditCampusViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère la fenêtre de modification d'un campus</summary>
    class EditCampusViewModel : BaseViewModel
    {
        public Campus campus { get; set; }
        public string name { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
        public Validation validName { get; set; }
        public ICommand cmdEdit { get; set; }
        public Action CloseActionAdd { get; set; }
      
        /// <summary>
        /// Initialise les valeurs à bindé et lie la commande d'édition avec l'action
        /// </summary>
        /// <param name="campus">Le campus à modifier</param>
        public EditCampusViewModel(Campus campus) {
            this.campus = campus;
            this.name = campus.name;
            this.street = campus.street;
            this.city = campus.city;
            this.zip = campus.zip;
            this.country = campus.country;
            this.cmdEdit = new RelayCommand<Object>(actEdit);
        }

        /// <summary>
        ///  Verification des champs saisis dans le formulaire et modification du campus
        /// </summary>
        /// <param name="o"></param>
        public void actEdit(Object o) {

            bool error = false;
            this.validName = new Validation();

            // Validation prénom
            if (!this.name.Equals(""))
            {
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

            if (!error)
            {
                // Enregistre dans la base
                campus.name = this.name;
                campus.street = this.street;
                campus.zip = this.zip;
                campus.city = this.city;
                campus.country = this.country;

                db.SaveChanges();

                this.CloseActionAdd();
            }
        }
    }
}
