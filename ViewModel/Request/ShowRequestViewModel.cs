using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Request
{
    class ShowRequestViewModel : BaseViewModel
    {
        public request request { get; set; }
        public ObservableCollection<proposition> propositions { get; set; }

        public bool isInscription { get; set; }

        public ICommand cmdEdit { get; set; }
        public ICommand cmdAddProposition { get; set; }
        public ICommand cmdDeleteProposition { get; set; }
        public ICommand cmdDeleteRequest { get; set; }
        public ICommand cmdSendProposition { get; set; }

        public ICommand cmdListEvent { get; set; }
        public ICommand cmdInscription { get; set; }

        public ShowRequestViewModel(request request)
        {
            this.request = request;
            this.propositions = new ObservableCollection<proposition>(request.propositions.ToList());

            isInscription = true;
            foreach (proposition p in propositions)
                if ((bool)p.inscription)
                {
                    isInscription = false;
                    break;
                }

            this.cmdEdit = new RelayCommand<request>(actEdit);
            this.cmdAddProposition = new RelayCommand<Object>(actAddProposition);
            this.cmdDeleteProposition = new RelayCommand<proposition>(actDeleteProposition);
            this.cmdSendProposition = new RelayCommand<Object>(actSendProposition);
            this.cmdDeleteRequest = new RelayCommand<Object>(actDeleteRequest);
            this.cmdListEvent = new RelayCommand<Object>(actListEvent);
            this.cmdInscription = new RelayCommand<proposition>(actInscription);

            mediator.Register(Helper.Event.ADD_REQUEST,this);
            mediator.Register(Helper.Event.DELETE_REQUEST, this);
            mediator.Register(Helper.Event.ADD_PROPOSITION,this);
            mediator.Register(Helper.Event.ADD_INSCRIPTION, this);
        }

        public void actSendProposition(Object request)
        {
            _event _event = new _event();
            _event.date = DateTime.Now;
            _event.event_types = db.event_types.Where(et => et.name == "Proposition").First();

            this.request.events.Add(_event);

            db.SaveChanges();
            NotifyPropertyChanged("request");

        }

        public void actEdit(request request)
        {
            mediator.openEditRequestView(this.request);
        }

        public void actAddProposition(Object o) {
            mediator.openAddPropositionView(this.request);
        }

        public void actDeleteProposition(proposition proposition) {
            db.propositions.Remove(proposition);
            propositions.Remove(proposition);
            NotifyPropertyChanged("propositions");
        }

        public void actDeleteRequest(Object o) {
           mediator.openDeleteRequestView(this.request);
        }

        public void actListEvent(Object o)
        {
            //mediator.openListEventView(this.request);
        }

        public void actInscription(proposition p)
        {

            mediator.openInscriptionView(p);

            /*
            _event _event = new _event();
            _event.date = DateTime.Now;
            _event.event_types = db.event_types.Where(et => et.name == "Inscription").First();

            p.inscription = true;
            db.SaveChanges();

            isInscription = false;
            NotifyPropertyChanged("isInscription");
            */
        }

        public override void Update(string eventName, object item)
        {
            switch (eventName)
            {
                case Helper.Event.ADD_PROPOSITION:
                    this.propositions.Add((proposition)item);
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
