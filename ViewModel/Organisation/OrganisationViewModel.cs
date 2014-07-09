using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
            this.organisationTabs = new ObservableCollection<Tab>();

            this.organisationTabs.Add(new Tab("Liste", mediator.openListOrganisationView(), null, null));
            mediator.Register(Helper.Event.DELETE_ORGANISATION, this);
        }

        private void actCloseTab(Tab tab)
        {
            this.organisationTabs.Remove(tab);
        }

        public void actAddTab(organisation organisation, UserControl view)
        {
            Tab tab = new Tab(organisation.name, view , organisation, null);
         
            organisationTabs.Add(tab);
            this.selectedTab = tab;

        }

        public override void Update(string eventName, Object item)
        {
            switch (eventName)
            {
                case Helper.Event.DELETE_ORGANISATION:

                    for (int i = 0; i < organisationTabs.Count(); i++)
                    {
                        if (organisationTabs[i].entity == item)
                        {
                            organisationTabs.Remove(organisationTabs[i]);
                            i--;
                        }
                    }

                    NotifyPropertyChanged("organisationTabs");
                    NotifyPropertyChanged("selectedTab");
                    break;
            }
        }
    }
}
