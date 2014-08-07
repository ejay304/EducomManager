using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisations
{
    /// <filename>DeleteOrganisationViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère la fenêtre de suppression d'organisations</summary>
    class DeleteOrganisationViewModel : BaseViewModel
    {
        public Organisation organisation { get; set; }
        public ICommand cmdArchive { get; set; }
        public ICommand cmdDelete { get; set; }
        public int nbrContact { get; set; }
        public int nbrProgram { get; set; }
  
        public Action CloseActionDelete { get; set; }

        /// <summary>
        /// Initialise les valeurs à binder et lie la commande de suppression à l'action
        /// </summary>
        /// <param name="organisation"></param>
        public DeleteOrganisationViewModel(Organisation organisation) {

            this.cmdDelete = new RelayCommand<Object>(actDelete);
            this.organisation = organisation;
            this.nbrContact = organisation.contacts.Count();
            this.nbrProgram = organisation.programs.Count();
        }

        /// <summary>
        /// Supprimer l'organisation et toutes ses dépendances
        /// </summary>
        /// <param name="obj"></param>
        private void actDelete(object obj)
        {
            /*for (int i = 0; i < nbrContact; i++)
            {
                db.contacts.Remove(organisation.contacts.First());
            }*/
            for (int i = 0; i < nbrProgram; i++)
            {
                db.programs.Remove(organisation.programs.First());
            }

            db.organisations.Remove(organisation);
            mediator.NotifyViewModel(Helper.Event.DELETE_ORGANISATION, organisation);
            db.SaveChanges();

            this.CloseActionDelete();
        }
    }
}
