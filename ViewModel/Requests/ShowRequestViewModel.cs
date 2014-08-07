using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Requests
{
    /// <filename>ShowRequestViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère le contrôle utilisateur qui affiche le détails d'une requête</summary>
    class ShowRequestViewModel : BaseViewModel
    {
        public Request request { get; set; }
        public ObservableCollection<Proposition> propositions { get; set; }

        public bool isInscription { get; set; }

        public ICommand cmdEdit { get; set; }
        public ICommand cmdAddProposition { get; set; }
        public ICommand cmdDeleteProposition { get; set; }
        public ICommand cmdDeleteRequest { get; set; }
        public ICommand cmdSendProposition { get; set; }
        public ICommand cmdListEvent { get; set; }
        public ICommand cmdInscription { get; set; }

        /// <summary>
        /// Initialise les valeurs à binder, lie les commande aux action concernée
        /// s'abonnes au événement le concernant
        /// </summary>
        /// <param name="request"></param>
        public ShowRequestViewModel(Request request)
        {
            this.request = request;
            this.propositions = new ObservableCollection<Proposition>(request.propositions.ToList());

            isInscription = true;
            foreach (Proposition p in propositions)
                if ((bool)p.inscription)
                {
                    isInscription = false;
                    break;
                }

            this.cmdEdit = new RelayCommand<Request>(actEdit);
            this.cmdAddProposition = new RelayCommand<Object>(actAddProposition);
            this.cmdDeleteProposition = new RelayCommand<Proposition>(actDeleteProposition);
            this.cmdSendProposition = new RelayCommand<Object>(actSendProposition);
            this.cmdDeleteRequest = new RelayCommand<Object>(actDeleteRequest);
            this.cmdListEvent = new RelayCommand<Object>(actListEvent);
            this.cmdInscription = new RelayCommand<Proposition>(actInscription);

            mediator.Register(Helper.Event.ADD_PROPOSITION, this);
            mediator.Register(Helper.Event.DELETE_PROPOSITION, this);
            mediator.Register(Helper.Event.ADD_INSCRIPTION, this);
        }

        /// <summary>
        /// Crée un événement de type Proposition
        /// </summary>
        /// <param name="request">la demande liée</param>
        public void actSendProposition(Object request)
        {
            Event _event = new Event();
            _event.date = DateTime.Now;
            _event.event_types = db.event_types.Where(et => et.name == "Proposition").First();

            this.request.events.Add(_event);

            db.SaveChanges();
            NotifyPropertyChanged("request");

        }

        /// <summary>
        /// Ouvre la fenêtre d'edition de demande
        /// </summary>
        /// <param name="request">La demande à modifier</param>
        public void actEdit(Request request)
        {
            mediator.openEditRequestView(this.request);
        }

        /// <summary>
        /// Ouvre la fenêtre d'ajout de proposition
        /// </summary>
        /// <param name="o"></param>
        public void actAddProposition(Object o) {
            mediator.openAddPropositionView(this.request);
        }

        /// <summary>
        /// Supprime la proposition 
        /// </summary>
        /// <param name="proposition">la proposition à supprimer</param>
        public void actDeleteProposition(Proposition proposition) {
            db.propositions.Remove(proposition);
            propositions.Remove(proposition);
            NotifyPropertyChanged("propositions");
        }

        /// <summary>
        /// Ouvre la fenêtre de suppression de demande
        /// </summary>
        /// <param name="o"></param>
        public void actDeleteRequest(Object o) {
           mediator.openDeleteRequestView(this.request);
        }

        /// <summary>
        /// Ouvre le fenêtre avec la liste des 
        /// </summary>
        /// <param name="o"></param>
        public void actListEvent(Object o)
        {
            mediator.openListEventView(this.request);
        }

        /// <summary>
        /// Ouvre la fenêtre d'inscription
        /// </summary>
        /// <param name="p"></param>
        public void actInscription(Proposition p)
        {
            mediator.openInscriptionView(p);
        }

        /// <summary>
        /// Fonction de mise à jour en cas de notification d'événement
        /// </summary>
        /// <param name="eventName">Le type d'événement</param>
        /// <param name="item">l'objet concerné par l'événement</param>
        public override void Update(string eventName, object item)
        {
            switch (eventName)
            {
                case Helper.Event.ADD_PROPOSITION:
                    this.propositions.Add((Proposition)item);
                    NotifyPropertyChanged("propositions");
                    break;
                case Helper.Event.DELETE_PROPOSITION:
                    this.propositions.Remove((Proposition)item);
                    NotifyPropertyChanged("propositions");
                    break;
               
                case Helper.Event.ADD_INSCRIPTION:
                    isInscription = false;
                    this.request.events.Add(((Proposition)item).request.state);
                    NotifyPropertyChanged("request");
                    NotifyPropertyChanged("isInscription");
                    break;
            }
        }
    }
}
