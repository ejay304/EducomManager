using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Requests
{
    /// <filename>DeleteRequestViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère la fenêtre de suppression de requêtes</summary>
    class DeleteRequestViewModel : BaseViewModel
    {
        public Request request { get; set; }
        public ICommand cmdArchive { get; set; }
        public ICommand cmdDelete { get; set; }
        public int nbrProposition { get; set; }
        public int nbrEvent { get; set; }

        public Action CloseActionDelete { get; set; }

        /// <summary>
        /// Initialise les valeuers à bindé et lie la commande de suppression à l'action
        /// </summary>
        /// <param name="request"></param>
        public DeleteRequestViewModel(Request request) {
            this.cmdDelete = new RelayCommand<Object>(actDelete);
            this.request = request;
            this.nbrProposition = request.propositions.Count;
            this.nbrEvent = request.events.Count;
        }

        /// <summary>
        /// Suppression de la demande et de toutes ces dépendences
        /// </summary>
        /// <param name="o"></param>
        public void actDelete(Object o)
        {
            int nbrResponse = request.responses.Count;

            for (int i = 0; i < nbrProposition; i++)
            {
                db.propositions.Remove(request.propositions.First());
            }
            for (int i = 0; i < nbrEvent; i++)
            {
                db.events.Remove(request.events.First());
            }
            for (int i = 0; i < nbrResponse; i++)
            {
                db.responses.Remove(request.responses.First());
            }
            db.requests.Remove(request);
            mediator.NotifyViewModel(Helper.Event.DELETE_REQUEST, request);
            db.SaveChanges();

            this.CloseActionDelete();
        
        }
    }
}
