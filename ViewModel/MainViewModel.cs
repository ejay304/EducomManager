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
            tabs = new ObservableCollection<Tab>();

            mediator.createTabViewModel();

            if(mediator.roleUser != Helper.Enum.User.assistant)
                tabs.Add(new Tab("Dashboard", mediator.TabUC["dashboard"],null, "../Ressource/dashboard.png"));


            tabs.Add(new Tab("Clients", mediator.TabUC["customer"],null, "../Ressource/clients.png"));

            tabs.Add(new Tab("Organisations", mediator.TabUC["organisation"], null, "../Ressource/organisations.png"));
        }
    }
}
