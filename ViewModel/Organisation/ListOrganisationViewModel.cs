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
        
        private ViewModel.Organisation.OrganisationViewModel parentVM;

        public ObservableCollection<organisation> organisations { get; set; }

        public int nbrCustomer 
        { 
            get { return this.organisations.Count; } 
        }

        public ICommand cmdViewDetail { get; set; }

        public ICommand cmdAdd { get; set; }

        public ListOrganisationViewModel(ViewModel.Organisation.OrganisationViewModel parentVM) : base()
        {
            this.parentVM = parentVM;
            this.organisations = new ObservableCollection<organisation>(db.organisations.ToList());
            this.cmdViewDetail = new RelayCommand<contact>(actViewDetail);
            this.cmdAdd = new RelayCommand<object>(actAdd);           
        }

        private void actAdd(object obj)
        {
            AddOrganisationViewModel addOrganisationViewModel = new AddOrganisationViewModel(this);
            AddOrganisationView addOrganisationView = new AddOrganisationView();

            addOrganisationView.DataContext = addOrganisationViewModel;
            addOrganisationViewModel.CloseActionFormAdd = new Action(() => addOrganisationView.Close());

            addOrganisationView.Show(); 
        }

        public void actViewDetail(contact customer)
        {

        }
    }
}
