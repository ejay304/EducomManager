using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisations.Programs.Campuses
{
    /// <filename>DeleteCampusViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère la fenêtre de suppression de campus</summary>
    class DeleteCampusViewModel : BaseViewModel 
    {
        public Campus campus { get; set; }
        public ICommand cmdArchive { get; set; }
        public ICommand cmdDelete { get; set; }
        public int nbrProposition { get; set; }
        public Action CloseActionDelete { get; set; }

        /// <summary>
        /// Initialise les valeurs a binder et lie la commande de suppression à l'action
        /// </summary>
        /// <param name="campus"></param>
        public DeleteCampusViewModel(Campus campus){
            this.campus = campus;
            //this.cmdArchive = new RelayCommand<Object>()
            this.cmdDelete = new RelayCommand<Object>(actDelete);
            this.nbrProposition = campus.propositions.Count();
        }

        /// <summary>
        /// Suppression de du campus et de ses dépendances
        /// </summary>
        /// <param name="o"></param>
        public void actDelete(Object o) {
            for (int i = 0; i < nbrProposition; i++)
            {
                db.propositions.Remove(campus.propositions.First());
            }
      

            mediator.NotifyViewModel(Helper.Event.DELETE_CAMPUS, campus);
            db.campus.Remove(campus);
            db.SaveChanges();

            this.CloseActionDelete();
        
        }
    
    }
}
