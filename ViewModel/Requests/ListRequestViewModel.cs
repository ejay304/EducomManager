using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Data.Entity;
using PrototypeEDUCOM.Helper;
using System.ComponentModel;

namespace PrototypeEDUCOM.ViewModel.Requests
{
    class ListRequestViewModel : BaseViewModel
    {
        public SortableObservableCollection<Request> requests { get; set; }

        public int nbrRequest
        { 
            get { return this.requests.Count; } 
        }

        private Dictionary<string, bool> directionSorted = new Dictionary<string, bool>();

        public List<User> advisers { get; set; }
        public List<EventType> states { get; set; }

        public User filterAdviser { get; set; }
        public EventType filterState { get; set; }

        public ICommand cmdViewDetail { get; set; }

        public ICommand cmdSort { get; set; }

        public ICommand cmdFilter { get; set; }

        /// <summary>
        /// Initialise les valeurs à bindé, ainsi que les listes pour les filtres
        /// lie les commmandes au actions concernée et s'abonne aux événements le concernant
        /// </summary>
        public ListRequestViewModel() : base()
        {

            this.requests = new SortableObservableCollection<Request>(db.requests.ToList());
            this.cmdViewDetail = new RelayCommand<Request>(actViewDetail);
            this.cmdSort = new RelayCommand<string>(actSort);
            this.cmdFilter = new RelayCommand<object>(actFilter);


            var query = from p in db.users
                        where p.role != "assistant"
                        select p;

            advisers = query.ToList();

            User allAdviser = new User();
            allAdviser.firstname = "TOUS";
            advisers.Insert(0, allAdviser);
            filterAdviser = allAdviser;

            states = db.event_types.ToList();
            EventType allState = new EventType();
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

        /// <summary>
        /// Ouvre l'onglet affichant le détail de la deamde
        /// </summary>
        /// <param name="request">la demande concernée</param>
        public void actViewDetail(Request request)
        {
            mediator.openShowRequestView(request);
        }

        /// <summary>
        /// Tri le contenu de la liste de demande
        /// </summary>
        /// <param name="arg">Le paramètre de tri</param>
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

        /// <summary>
        /// Filtre la liste de demande
        /// </summary>
        /// <param name="obj">le paramètre de filtre</param>
        private void actFilter(object obj)
        {
            var query = from p in db.requests
                        select p;

            if (filterAdviser.firstname != "TOUS")
                query = query.Where(c => (c.user.lastname == filterAdviser.lastname && c.user.firstname == filterAdviser.firstname));

            if (filterState.name != "TOUS")
                query = query.Where(c => c.events.Any(e => e.event_types_id == filterState.id));
         

            this.requests = new SortableObservableCollection<Request>(query.ToList());
            NotifyPropertyChanged("requests");
            NotifyPropertyChanged("nbrRequest");
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
                case Helper.Event.ADD_REQUEST:

                    // Ajoute dans la liste
                    this.requests.Add((Request)item);
                    NotifyPropertyChanged("requests");
                    NotifyPropertyChanged("nbrRequest");
                    break;
                case Helper.Event.DELETE_REQUEST:

                    // Ajoute dans la liste
                    this.requests.Remove((Request)item);
                    NotifyPropertyChanged("requests");
                    NotifyPropertyChanged("nbrRequest");
                    break;
            }
        }
    }
}
