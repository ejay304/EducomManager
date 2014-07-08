using PrototypeEDUCOM.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisation
{
    class OrganisationViewModel : BaseViewModel
    {
        private Tab _selectedTab;

        public ICommand cmdCloseTab { get; set; }
        public ObservableCollection<Tab> organisationTabs { get; set; }
        public Tab selectedTab {
            get { return _selectedTab;  }
            set {
                _selectedTab = value;
                NotifyPropertyChanged("selectedTab");
            }
        }

        public OrganisationViewModel()
        {

            this.cmdCloseTab = new RelayCommand<Tab>(actCloseTab);
            organisationTabs = new ObservableCollection<Tab>();

            View.Organisation.ListOrganisationUCView view = new View.Organisation.ListOrganisationUCView();
            view.DataContext = new ViewModel.Organisation.ListOrganisationViewModel(this);

            organisationTabs.Add(new Tab("Liste", view,null, null));
        }

        private void actCloseTab(Tab tab)
        {
            organisationTabs.Remove(tab);
            NotifyPropertyChanged("customerTabs");
        }
    }
}
