using PrototypeEDUCOM.Helper;
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
                tabs.Add("dashboard",new Tab("Dashboard", mediator.mainTabs["dashboard"].tabUC, null, "../Ressource/dashboard.png"));

            tabs.Add("customer",new Tab("Clients", mediator.mainTabs["customer"].tabUC, null, "../Ressource/clients.png"));
            tabs.Add("organisation",new Tab("Organisations", mediator.mainTabs["organisation"].tabUC, null, "../Ressource/organisations.png"));
            tabs.Add("request",new Tab("Demandes", mediator.mainTabs["request"].tabUC, null, "../Ressource/demandes.png"));

            if (Helper.Enum.User.assistant == Helper.Enum.User.list[Helper.Enum.User.indexByValue(mediator.user.role)])
                this.selectedTab = tabs["customer"];
            else
                this.selectedTab = tabs["dashboard"];
        }
    }
}
