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

        public void actSendProposition(Object request)
        {
            Event _event = new Event();
            _event.date = DateTime.Now;
            _event.event_types = db.event_types.Where(et => et.name == "Proposition").First();

            this.request.events.Add(_event);

            db.SaveChanges();
            NotifyPropertyChanged("request");

        }

        public void actEdit(Request request)
        {
            mediator.openEditRequestView(this.request);
        }

        public void actAddProposition(Object o) {
            mediator.openAddPropositionView(this.request);
        }

        public void actDeleteProposition(Proposition proposition) {
            db.propositions.Remove(proposition);
            propositions.Remove(proposition);
            NotifyPropertyChanged("propositions");
        }

        public void actDeleteRequest(Object o) {
           mediator.openDeleteRequestView(this.request);
        }

        public void actListEvent(Object o)
        {
            mediator.openListEventView(this.request);
        }

        public void actInscription(Proposition p)
        {
            mediator.openInscriptionView(p);
        }

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
                    NotifyPropertyChanged("isInscription");
                    break;
            }
        }
    }
}
