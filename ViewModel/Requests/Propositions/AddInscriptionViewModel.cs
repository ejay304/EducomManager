using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Requests.Propositions
{
    /// <filename>AddInscriptionViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère la fenêtre d'inscription d'une recommandation</summary>
    class AddInscriptionViewModel : BaseViewModel
    {
        public Proposition proposition { get; set; }
        public Campus campus { get; set; }

        public ICommand cmdAdd { get; set; }
        public Action CloseActionAdd { get; set; }

        /// <summary>
        /// Initialise les valeurs à binder et lie la commande d'ajout à l'action
        /// </summary>
        /// <param name="proposition"></param>
        public AddInscriptionViewModel(Proposition proposition) 
        {
            this.proposition = proposition;
            this.campus = proposition.program.campus.First();
            this.cmdAdd = new RelayCommand<Campus>(actAdd);
        }

        /// <summary>
        /// Ajout de l'événement inscription et indique le campus dans la proposition
        /// </summary>
        /// <param name="c"></param>
        public void actAdd(Campus c)
        {
            Event _event = new Event();
            _event.date = DateTime.Now;
            _event.event_types = db.event_types.Where(et => et.name == "Inscription").First();
            this.proposition.request.events.Add(_event);

            this.proposition.inscription = true;
            this.proposition.campu = campus;
            db.SaveChanges();

            mediator.NotifyViewModel(Helper.Event.ADD_INSCRIPTION, proposition);
            this.CloseActionAdd();
        }
    }
}
