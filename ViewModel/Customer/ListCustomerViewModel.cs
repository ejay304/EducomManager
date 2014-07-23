using PrototypeEducom.Helper;
using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Model;
using PrototypeEDUCOM.View.Customer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Data.Entity;

namespace PrototypeEDUCOM.ViewModel.Customer
{
    class ListCustomerViewModel : BaseViewModel
    {

        public SortableObservableCollection<contact> customers { get; set; }

        public int nbrCustomer 
        { 
            get { return this.customers.Count; } 
        }

        private Dictionary<string,bool> directionSorted = new Dictionary<string,bool>();

        public ICommand cmdViewDetail { get; set; }

        public ICommand cmdAdd { get; set; }

        public ICommand cmdSort { get; set; }
        public ICommand test { get; set; }
        public Action CloseActionFormEdit { get; set; }

        public ListCustomerViewModel() : base()
        {
            this.customers = new SortableObservableCollection<contact>(db.contacts
                .Include(c => c.requests.Select(r => r.events.Select(e => e.event_types)))
                .Include(c => c.emails)
                //.Include(c => c.)
                .ToList());

            this.cmdViewDetail = new RelayCommand<contact>(actViewDetail);
            this.cmdAdd = new RelayCommand<object>(actAdd);
            this.cmdSort = new RelayCommand<string>(actSort);
            this.test = new RelayCommand<string>(actSort);
            mediator.Register(Helper.Event.ADD_CUSTOMER, this);

            directionSorted.Add("firstname", false);
            directionSorted.Add("lastname", false);
        }

        private void actAdd(object obj)
        {
            mediator.openAddCustomerView();
        }

        private void actSort(string arg)
        {

            ListSortDirection direction;

            direction = directionSorted[arg] ? ListSortDirection.Descending : ListSortDirection.Ascending;
            directionSorted[arg] = !directionSorted[arg];

            switch (arg)
            {
                case "firstname" :
                    this.customers.Sort(c => c.firstname, direction);
                    break;
                case "lastname":
                    this.customers.Sort(c => c.lastname, direction);
                    break;
            }
            
            NotifyPropertyChanged("customers");
        }

        public void actViewDetail(contact customer)
        {
            mediator.openShowCustomerView(customer);
            mediator.Register(Helper.Event.DELETE_CUSTOMER, this);
            mediator.Register(Helper.Event.DELETE_CUSTOMER, mediator.mainTabs[TabName.CUSTOMER].tabViewModel);
        }

        public override void Update(string eventName, object item)
        {
            switch (eventName)
            {
                case Helper.Event.ADD_CUSTOMER :

                    // Ajoute dans la liste
                    this.customers.Add((contact)item);
                    NotifyPropertyChanged("customers");
                    NotifyPropertyChanged("nbrCustomer");
                    break;
                case Helper.Event.DELETE_CUSTOMER :

                    // Ajoute dans la liste
                    this.customers.Remove((contact)item);
                    NotifyPropertyChanged("customers");
                    NotifyPropertyChanged("nbrCustomer");
                    break;
            }
        }
    }
}
