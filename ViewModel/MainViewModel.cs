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
        public MainViewModel()
        {

            //SUPRIMER LORS DE LA MISE EN PROD !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            mediator.user = db.users.First();




            tabs = new ObservableCollection<Tab>();

            mediator.createTabViewModel();

            if (Helper.Enum.User.assistant != Helper.Enum.User.list[Helper.Enum.User.indexByValue(mediator.user.role)])
                tabs.Add(new Tab("Dashboard", mediator.mainTabs["dashboard"].tabUC, null, "../Ressource/dashboard.png"));


            tabs.Add(new Tab("Clients", mediator.mainTabs["customer"].tabUC, null, "../Ressource/clients.png"));
            tabs.Add(new Tab("Organisations", mediator.mainTabs["organisation"].tabUC, null, "../Ressource/organisations.png"));
            tabs.Add(new Tab("Demandes", mediator.mainTabs["request"].tabUC, null, "../Ressource/demandes.png"));

        }
    }
}
