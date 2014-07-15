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
    class ListRequestViewModel : BaseViewModel
    {
        public ObservableCollection<request> requests { get; set; }

        public int nbrOrganisation
        { 
            get { return this.requests.Count; } 
        }

        public ICommand cmdViewDetail { get; set; }

        public ListRequestViewModel() : base()
        {
            this.requests = new ObservableCollection<request>(db.requests.ToList());
            this.cmdViewDetail = new RelayCommand<request>(actViewDetail);

            mediator.Register(Helper.Event.ADD_REQUEST, this);
            mediator.Register(Helper.Event.DELETE_REQUEST, this);
        }

        public void actViewDetail(request request)
        {
            mediator.openShowRequestView(request);
        }


        public override void Update(string eventName, object item)
        {
            switch (eventName)
            {
                case Helper.Event.ADD_ORGANISATION:

                    // Ajoute dans la liste
                    this.requests.Add((request)item);
                    NotifyPropertyChanged("requests");
                    NotifyPropertyChanged("nbrRequest");
                    break;

                case Helper.Event.DELETE_ORGANISATION:

                    // Ajoute dans la liste
                    this.requests.Remove((request)item);
                    NotifyPropertyChanged("requests");
                    NotifyPropertyChanged("nbrRequest");
                    break;
            }
        }
    }
}
