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
            organisationTabs = new ObservableCollection<Tab>();

            organisationTabs.Add(new Tab("Liste", mediator.openListOrganisationView(), null, null));
        }

        private void actCloseTab(Tab tab)
        {
            organisationTabs.Remove(tab);
            NotifyPropertyChanged("organisationTabs");
        }

        public void actAddTab(organisation organisation, UserControl view)
        {
            Tab tab = new Tab(organisation.name, view, organisation, null);

            this.organisationTabs.Add(tab);
            this.selectedTab = tab;

            mediator.Register(Helper.Event.DELETE_ORGANISATION, this);
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

                    selectedTab = tabs.First();
                    NotifyPropertyChanged("organisationTabs");
                    NotifyPropertyChanged("selectedTab");
                    break;
            }
        }
    }
}
