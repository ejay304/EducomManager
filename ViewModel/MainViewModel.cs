using PrototypeEDUCOM.Helper;
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
    class MainViewModel : BaseViewModel
    {
        public user user { get { return mediator.user; } }

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

        public MainViewModel()
        {

            tabs = new Dictionary<String, Tab>();

            mediator.createTabViewModel();

            if (Helper.Enum.User.assistant != Helper.Enum.User.list[Helper.Enum.User.indexByValue(mediator.user.role)])
                tabs.Add(TabName.DASHBORAD,new Tab("Dashboard", mediator.mainTabs["dashboard"].tabUC, null, "../Ressource/dashboard.png"));

            tabs.Add(TabName.CUSTOMER,new Tab("Clients", mediator.mainTabs["customer"].tabUC, null, "../Ressource/clients.png"));
            tabs.Add(TabName.ORGANISATION, new Tab("Organisations", mediator.mainTabs["organisation"].tabUC, null, "../Ressource/organisations.png"));
            tabs.Add(TabName.REQUEST, new Tab("Demandes", mediator.mainTabs["request"].tabUC, null, "../Ressource/demandes.png"));

            if (Helper.Enum.User.assistant == Helper.Enum.User.list[Helper.Enum.User.indexByValue(mediator.user.role)])
                this.selectedTab = tabs[TabName.CUSTOMER];
            else
                this.selectedTab = tabs[TabName.DASHBORAD];
        }
    }
}
