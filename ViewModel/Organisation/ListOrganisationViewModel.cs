using PrototypeEDUCOM.Model;
using PrototypeEDUCOM.View.Organisation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisation
{
    class ListOrganisationViewModel : BaseViewModel
    {
        public ObservableCollection<organisation> organisations { get; set; }

        public int nbrOrganisation
        { 
            get { return this.organisations.Count; } 
        }

        public ICommand cmdViewDetail { get; set; }

        public ICommand cmdAdd { get; set; }

        public ListOrganisationViewModel() : base()
        {
            this.organisations = new ObservableCollection<organisation>(db.organisations.ToList());
            this.cmdViewDetail = new RelayCommand<organisation>(actViewDetail);
            this.cmdAdd = new RelayCommand<object>(actAdd);

            mediator.Register(Helper.Event.ADD_ORGANISATION, this);
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
                case Helper.Event.ADD_CUSTOMER:

                    // Ajoute dans la liste
                    this.organisations.Add((organisation)item);
                    NotifyPropertyChanged("organisations");
                    NotifyPropertyChanged("nbrOrganisation");
                    break;
                case Helper.Event.DELETE_CUSTOMER:

                    // Ajoute dans la liste
                    this.organisations.Remove((organisation)item);
                    NotifyPropertyChanged("organisations");
                    NotifyPropertyChanged("nbrOrganisation");
                    break;
            }
        }
    }
}
