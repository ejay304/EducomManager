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

namespace PrototypeEDUCOM.ViewModel.Requests
{
    /// <filename>RequestViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère le contrôle utilisateur de l'onglet Demande</summary>
    class RequestViewModel : BaseViewModel
    {
        private Tab _selectedTab;

        public ICommand cmdCloseTab { get; set; }
        public ObservableCollection<Tab> requestTabs { get; set; }
        public Tab selectedTab {
            get { return _selectedTab;  }
            set {
                _selectedTab = value;
                NotifyPropertyChanged("selectedTab");
            }
        }

        public RequestViewModel()
        {
            this.cmdCloseTab = new RelayCommand<Tab>(actCloseTab);
            this.requestTabs = new ObservableCollection<Tab>();

            this.requestTabs.Add(new Tab("Liste", mediator.openListRequestView(), null, null,true));

            mediator.Register(Helper.Event.DELETE_REQUEST, this);
            mediator.Register(Helper.Event.DELETE_CUSTOMER, this);
            mediator.Register(Helper.Event.DELETE_ORGANISATION, this);
            mediator.Register(Helper.Event.DELETE_PROGRAM, this);
     
     
        }

        private void actCloseTab(Tab tab)
        {
            this.requestTabs.Remove(tab);
        }

        public void actAddTab(Request request, UserControl view)
        {
            foreach (Tab t in requestTabs)
            {
                if (t.entity == request)
                {
                    this.selectedTab = t;
                    return;
                }
            }

            Tab tab = new Tab(request.creation_date.ToString(), view , request, null);
         
            requestTabs.Add(tab);
            this.selectedTab = tab;
        }

        public override void Update(string eventName, Object item)
        {
            switch (eventName)
            {
                case Helper.Event.DELETE_REQUEST:

                    for (int i = 0; i < requestTabs.Count(); i++)
                    {
                        if (requestTabs[i].entity == item)
                        {
                            requestTabs.Remove(requestTabs[i]);
                            i--;
                        }
                    }

                    NotifyPropertyChanged("requestTabs");
                    NotifyPropertyChanged("selectedTab");
                    break;
                case Helper.Event.DELETE_CUSTOMER:
                    for (int i = 0; i < requestTabs.Count(); i++)
                    {
                        foreach (Request request in ((Contact)item).requests)
                        {
                            if (requestTabs[i].entity == request)
                            {
                                requestTabs.Remove(requestTabs[i]);
                                i--;
                            }
                        }
                    }
                    NotifyPropertyChanged("requestTabs");
                    NotifyPropertyChanged("selectedTab");
                    break;
            }
        }
    }
}
