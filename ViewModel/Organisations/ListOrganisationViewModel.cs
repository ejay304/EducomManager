using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Model;
using PrototypeEDUCOM.View.Organisations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisations
{
    class ListOrganisationViewModel : BaseViewModel
    {
        public SortableObservableCollection<Organisation> organisations { get; set; }

        public int nbrOrganisation
        { 
            get { return this.organisations.Count; } 
        }

        private Dictionary<string, bool> directionSorted = new Dictionary<string, bool>();

        public Dictionary<string, string> countries { get; set; }

        public string filterCountry { get; set; }

        public ICommand cmdFilter { get; set; }
        public ICommand cmdSort { get; set; }

        public ICommand cmdViewDetail { get; set; }

        public ICommand cmdAdd { get; set; }

        /// <summary>
        /// Initialise les valeurs à bindé, initialise les listes de filtre
        /// lie les commandes aux action concernées et s'abonne au événement le concernant
        /// </summary>
        public ListOrganisationViewModel() : base()
        {
            this.organisations = new SortableObservableCollection<Organisation>(db.organisations.ToList());
            this.cmdViewDetail = new RelayCommand<Organisation>(actViewDetail);
            this.cmdFilter = new RelayCommand<object>(actFilter);
            this.cmdSort = new RelayCommand<string>(actSort);
            this.cmdAdd = new RelayCommand<object>(actAdd);

            directionSorted.Add("name", false);
            directionSorted.Add("city", false);
            directionSorted.Add("country", false);

            countries = new Dictionary<string, string>();
            countries.Add("all", "TOUS");
            countries = countries.Concat(Dictionaries.countries).ToDictionary(pair => pair.Key, pair => pair.Value);
            filterCountry = countries.First().Key;

            mediator.Register(Helper.Event.ADD_ORGANISATION, this);
            mediator.Register(Helper.Event.DELETE_ORGANISATION, this);

        }

        /// <summary>
        /// Filtre la liste d'organisation
        /// </summary>
        /// <param name="obj">le paramètre de filtre</param>
        private void actFilter(object obj)
        {
            var query = from p in db.organisations
                        select p;

            if (filterCountry != "all")
                query = query.Where(c => c.country == filterCountry);

            this.organisations = new SortableObservableCollection<Organisation>(query.ToList());
            NotifyPropertyChanged("organisations");
            NotifyPropertyChanged("nbrOrganisation");
        }

        /// <summary>
        /// Trie la liste d'organisation
        /// </summary>
        /// <param name="arg">le paramètre de tri</param>
        private void actSort(string arg)
        {

            ListSortDirection direction;

            direction = directionSorted[arg] ? ListSortDirection.Descending : ListSortDirection.Ascending;
            directionSorted[arg] = !directionSorted[arg];

            switch (arg)
            {
                case "name":
                    this.organisations.Sort(c => c.name, direction);
                    break;
                case "city":
                    this.organisations.Sort(c => c.city, direction);
                    break;
                case "country":
                    this.organisations.Sort(c => c.country, direction);
                    break;
            }

            NotifyPropertyChanged("organisations");
        }

        /// <summary>
        /// Ouvre la fenêtre d'ajout d'organisation
        /// </summary>
        /// <param name="obj"></param>
        private void actAdd(object obj)
        {
            mediator.openAddOrganisationView();
        }

        /// <summary>
        /// Ouvre l'onglet du détail de l'organisation
        /// </summary>
        /// <param name="organisation">L'organisation concernée</param>
        public void actViewDetail(Organisation organisation)
        {
            mediator.openShowOrganisationView(organisation);
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
                case Helper.Event.ADD_ORGANISATION:

                    // Ajoute dans la liste
                    this.organisations.Add((Organisation)item);
                    NotifyPropertyChanged("organisations");
                    NotifyPropertyChanged("nbrOrganisation");
                    break;
                case Helper.Event.DELETE_ORGANISATION:

                    // Retire de la liste
                    this.organisations.Remove((Organisation)item);
                    NotifyPropertyChanged("organisations");
                    NotifyPropertyChanged("nbrOrganisation");
                    break;
            }
        }
    }
}
