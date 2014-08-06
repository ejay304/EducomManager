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

namespace PrototypeEDUCOM.ViewModel.Organisations
{
    /// <filename>OrganisationViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère le contrôle utilisateur de l'onglet Organisation</summary>
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

            this.organisationTabs.Add(new Tab("Liste", mediator.openListOrganisationView(), null, null,true));
            mediator.Register(Helper.Event.DELETE_ORGANISATION, this);
            mediator.Register(Helper.Event.DELETE_PROGRAM, this);
     
        }

        private void actCloseTab(Tab tab)
        {
            this.organisationTabs.Remove(tab);
        }

        public void actAddTab(Organisation organisation, UserControl view)
        {
            foreach (Tab t in organisationTabs)
            {
                if (t.entity == organisation)
                {
                    this.selectedTab = t;
                    return;
                }
            }

            Tab tab = new Tab(organisation.name, view , organisation, null);
         
            organisationTabs.Add(tab);
            this.selectedTab = tab;

        }
       
        public void actAddTab(Program program, UserControl view)
        {
            foreach (Tab t in organisationTabs)
            {
                if (t.entity == program)
                {
                    this.selectedTab = t;
                    return;
                }
            }

            Tab tab = new Tab(program.organisation.name + " - " + program.program_types.name , view, program, null);

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
                case Helper.Event.DELETE_PROGRAM:
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
