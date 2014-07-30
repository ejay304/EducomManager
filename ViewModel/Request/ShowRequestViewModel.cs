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
        public ICommand cmdListEvent { get; set; }
        public ICommand cmdInscription { get; set; }

        public ShowRequestViewModel(request request)
        {
            this.request = request;
            this.propositions = new ObservableCollection<proposition>(request.propositions.ToList());

            isInscription = false;
            foreach (proposition p in propositions)
                if ((bool)p.inscription)
                {
                    isInscription = true;
                    break;
                }

            this.cmdEdit = new RelayCommand<request>(actEdit);
            this.cmdAddProposition = new RelayCommand<Object>(actAddProposition);
            this.cmdDeleteProposition = new RelayCommand<proposition>(actDeleteProposition);
            this.cmdDeleteRequest = new RelayCommand<Object>(actDeleteRequest);
            this.cmdListEvent = new RelayCommand<Object>(actListEvent);
            this.cmdInscription = new RelayCommand<proposition>(actInscription);

            mediator.Register(Helper.Event.ADD_REQUEST,this);
            mediator.Register(Helper.Event.DELETE_REQUEST, this);
           
            mediator.Register(Helper.Event.ADD_PROPOSITION,this);
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
            //
        }

        public override void Update(string eventName, object item)
        {
            switch (eventName)
            {
                case Helper.Event.ADD_PROPOSITION:
                    this.propositions.Add((proposition)item);
                    NotifyPropertyChanged("propositions");
                    break;
            }
        }
    }
}
