using PrototypeEducom.Helper;
using PrototypeEDUCOM.Model;
using PrototypeEDUCOM.View.Organisation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisation
{
    class ListOrganisationViewModel : BaseViewModel
    {
        public SortableObservableCollection<organisation> organisations { get; set; }

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

        public ListOrganisationViewModel() : base()
        {
            this.organisations = new SortableObservableCollection<organisation>(db.organisations.ToList());
            this.cmdViewDetail = new RelayCommand<organisation>(actViewDetail);
            this.cmdFilter = new RelayCommand<object>(actFilter);
            this.cmdSort = new RelayCommand<string>(actSort);
            this.cmdAdd = new RelayCommand<object>(actAdd);

            mediator.Register(Helper.Event.ADD_ORGANISATION, this);
            mediator.Register(Helper.Event.DELETE_ORGANISATION, this);

            directionSorted.Add("name", false);
            directionSorted.Add("city", false);
            directionSorted.Add("country", false);

            countries = new Dictionary<string, string>();
            countries.Add("suisse", "Suisse");
            countries.Add("france", "France");
            countries.Add("italie", "Italie");
        }

        private void actFilter(object obj)
        {
            var query = from p in db.organisations
                        select p;

            if (filterCountry != null)
                query = query.Where(c => c.country == filterCountry);

            this.organisations = new SortableObservableCollection<organisation>(query.ToList());
            NotifyPropertyChanged("organisations");
            NotifyPropertyChanged("nbrOrganisation");
        }

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

        private void actAdd(object obj)
        {
            mediator.openAddOrganisationView();
        }

        public void actViewDetail(organisation organisation)
        {
            mediator.openShowOrganisationView(organisation);
        }


        public override void Update(string eventName, object item)
        {
            switch (eventName)
            {
                case Helper.Event.ADD_ORGANISATION:

                    // Ajoute dans la liste
                    this.organisations.Add((organisation)item);
                    NotifyPropertyChanged("organisations");
                    NotifyPropertyChanged("nbrOrganisation");
                    break;
                case Helper.Event.DELETE_ORGANISATION:

                    // Ajoute dans la liste
                    this.organisations.Remove((organisation)item);
                    NotifyPropertyChanged("organisations");
                    NotifyPropertyChanged("nbrOrganisation");
                    break;
            }
        }
    }
}
