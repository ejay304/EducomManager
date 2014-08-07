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
    /// <filename>AddCampusViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère la fenêtre d'ajout de campus</summary>
    class AddCampusViewModel : BaseViewModel 
    {
        public Program program { get; set; }
        public string name { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
        public Validation validName { get; set; }
        public ICommand cmdAdd { get; set; }
        public Action CloseActionAdd { get; set; }
      
        /// <summary>
        /// Initialisation des valueurs bindé et lie la commande à l'action
        /// </summary>
        /// <param name="program">le programe lié au campus</param>
        public AddCampusViewModel(Program  program) {
            this.program = program;
            this.cmdAdd = new RelayCommand<Object>(actAdd);
        }

        /// <summary>
        ///     Verification des champs saisis dans le formulaire et ajout du campus
        /// </summary>
        public void actAdd(Object o) {

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
                Campus campus = new Campus();

                campus.name = this.name;
                campus.street = this.street;
                campus.zip = this.zip;
                campus.city = this.city;
                campus.country = this.country;

                // Enregistre dans la base
                program.campus.Add(campus);

                db.SaveChanges();
                mediator.NotifyViewModel(Helper.Event.ADD_CAMPUS, campus);

                this.CloseActionAdd();
            }
        }
    }
}
