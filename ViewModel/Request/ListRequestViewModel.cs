using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Data.Entity;
using PrototypeEducom.Helper;
using System.ComponentModel;

namespace PrototypeEDUCOM.ViewModel.Request
{
    class ListRequestViewModel : BaseViewModel
    {
        public SortableObservableCollection<request> requests { get; set; }

        public int nbrRequest
        { 
            get { return this.requests.Count; } 
        }

        private Dictionary<string, bool> directionSorted = new Dictionary<string, bool>();

        public List<user> advisers { get; set; }
        public List<event_types> states { get; set; }

        public user filterAdviser { get; set; }
        public event_types filterState { get; set; }

        public ICommand cmdViewDetail { get; set; }

        public ICommand cmdSort { get; set; }

        public ICommand cmdFilter { get; set; }

        public ListRequestViewModel() : base()
        {

            this.requests = new SortableObservableCollection<request>(db.requests.ToList());
            this.cmdViewDetail = new RelayCommand<request>(actViewDetail);
            this.cmdSort = new RelayCommand<string>(actSort);
            this.cmdFilter = new RelayCommand<object>(actFilter);


            var query = from p in db.users
                        where p.role == "adviser"
                        select p;

            advisers = query.ToList();

            user allAdviser = new user();
            allAdviser.firstname = "TOUS";
            advisers.Insert(0, allAdviser);
            filterAdviser = allAdviser;

            states = db.event_types.ToList();
            event_types allState = new event_types();
            allState.name = "TOUS";
            states.Insert(0, allState);
            filterState = allState;

            directionSorted.Add("customer", false);
            directionSorted.Add("student", false);
            directionSorted.Add("date", false);
            directionSorted.Add("journey", false);
            directionSorted.Add("adviser", false);
            directionSorted.Add("state", false);

            mediator.Register(Helper.Event.ADD_REQUEST, this);
            mediator.Register(Helper.Event.DELETE_REQUEST, this);
        }

        public void actViewDetail(request request)
        {
            mediator.openShowRequestView(request);
        }

        private void actSort(string arg)
        {

            ListSortDirection direction;

            direction = directionSorted[arg] ? ListSortDirection.Descending : ListSortDirection.Ascending;
            directionSorted[arg] = !directionSorted[arg];

            switch (arg)
            {
                case "customer":
                    this.requests.Sort(c => c.contact.fullName, direction);
                    break;
                case "student":
                    this.requests.Sort(c => c.student.fullName, direction);
                    break;
                case "date":
                    this.requests.Sort(c => c.creation_date, direction);
                    break;
                case "journey":
                    this.requests.Sort(c => c.journey_type, direction);
                    break;
                case "adviser":
                    this.requests.Sort(c => c.user.fullName, direction);
                    break;
                case "state":
                    this.requests.Sort(c => c.state.event_types.order, direction);
                    break;
            }
        }

        private void actFilter(object obj)
        {
            var query = from p in db.requests
                        select p;

            if (filterAdviser.firstname != "TOUS")
                query = query.Where(c => (c.user.lastname == filterAdviser.lastname && c.user.firstname == filterAdviser.firstname));

            if (filterState.name != "TOUS")
                query = query.Where(c => c.events.Any(e => e.event_types_id == filterState.id));
         

            this.requests = new SortableObservableCollection<request>(query.ToList());
            NotifyPropertyChanged("requests");
            NotifyPropertyChanged("nbrRequest");
        }

        public override void Update(string eventName, object item)
        {
            switch (eventName)
            {
                case Helper.Event.ADD_REQUEST:

                    // Ajoute dans la liste
                    this.requests.Add((request)item);
                    NotifyPropertyChanged("requests");
                    NotifyPropertyChanged("nbrRequest");
                    break;
                case Helper.Event.DELETE_REQUEST:

                    // Ajoute dans la liste
                    this.requests.Remove((request)item);
                    NotifyPropertyChanged("requests");
                    NotifyPropertyChanged("nbrRequest");
                    break;
            }
        }
    }
}
