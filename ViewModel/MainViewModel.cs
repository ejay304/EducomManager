﻿using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel
{
    /// <filename>MainViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère la fenêtre principale de l'application</summary>
    class MainViewModel : BaseViewModel
    {
        public User user { get { return mediator.user; } }

        private Tab _selectedTab;
        public Tab selectedTab
        {
            get { return _selectedTab; }
            set
            {
                _selectedTab = value;
                NotifyPropertyChanged("selectedTab");
            }
        }

        public ICommand cmdLogout { get; set; }

        public Action CloseWindow { get; set; }

        /// <summary>
        /// Ajoute les onglets en fonction de l'utilisateur connecté
        /// </summary>
        public MainViewModel()
        {

            this.cmdLogout = new RelayCommand<object>(actLogout);        

            tabs = new Dictionary<String, Tab>();

            mediator.createTabViewModel();

            if (!mediator.user.role.Equals("assistant"))
                tabs.Add(TabName.DASHBORAD,new Tab("Dashboard", mediator.mainTabs["dashboard"].tabUC, null, "../Ressource/dashboard.png"));

            tabs.Add(TabName.CUSTOMER,new Tab("Clients", mediator.mainTabs["customer"].tabUC, null, "../Ressource/clients.png"));
            tabs.Add(TabName.ORGANISATION, new Tab("Organisations", mediator.mainTabs["organisation"].tabUC, null, "../Ressource/organisations.png"));
            tabs.Add(TabName.REQUEST, new Tab("Demandes", mediator.mainTabs["request"].tabUC, null, "../Ressource/demandes.png"));

            if (mediator.user.role.Equals("administrator"))
                tabs.Add(TabName.ADMIN, new Tab("Admin", mediator.mainTabs["admin"].tabUC, null, "../Ressource/admin.png"));

            if (mediator.user.role.Equals("assistant"))
                this.selectedTab = tabs[TabName.CUSTOMER];
            else
                this.selectedTab = tabs[TabName.DASHBORAD];
        }

        /// <summary>
        /// Déconnecte l'utilisateur actuel.
        /// </summary>
        /// <param name="obj"></param>
        public void actLogout(object obj)
        {
            mediator.openLoginView();
            this.CloseWindow();
        }
    }
}
