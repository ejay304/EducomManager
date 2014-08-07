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

namespace PrototypeEDUCOM.ViewModel.Customers
{
    /// <filename>CustomerViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère le contrôle utilisateur de l'onglet Client</summary>
    class CustomerViewModel : BaseViewModel
    {

        private Tab _selectedTab;

        public ICommand cmdCloseTab { get; set; }
        public ObservableCollection<Tab> customerTabs { get; set; }
        public Tab selectedTab {
            get { return _selectedTab;  }
            set {
                _selectedTab = value;
                NotifyPropertyChanged("selectedTab");
            }
        }
        /// <summary>
        /// Initilise les onglets et d'abonne à la suppresion de client
        /// </summary>
        public CustomerViewModel()
        {
            this.cmdCloseTab = new RelayCommand<Tab>(actCloseTab);
            customerTabs = new ObservableCollection<Tab>();
            customerTabs.Add(new Tab("Liste", mediator.openListCustomerView(), null, null,true));

            mediator.Register(Helper.Event.DELETE_CUSTOMER, this);
        }

        /// <summary>
        /// Permet de retirer un onglet 
        /// </summary>
        /// <param name="tab">l'onglet à retirer</param>
        private void actCloseTab(Tab tab)
        {
            customerTabs.Remove(tab);
            NotifyPropertyChanged("customerTabs");
        }

        /// <summary>
        /// Permet d'ajouter un onglet 
        /// </summary>
        /// <param name="customer">l'entity concerné par l'onglet</param>
        /// <param name="view">le UserControle à afficher</param>
        public void actAddTab(Contact customer, UserControl view)
        {
            foreach (Tab t in customerTabs)
            {
                if (t.entity == customer)
                {
                    this.selectedTab = t;
                    return;
                }
            }

            Tab tab = new Tab(customer.fullName, view,customer, null);

            this.customerTabs.Add(tab);
            this.selectedTab = tab;
        }

        /// <summary>
        /// Fonction de mise à jour en cas de notification d'événement
        /// </summary>
        /// <param name="eventName">Le type d'événement</param>
        /// <param name="item">l'objet concerné par l'événement</param>
        public override void Update(string eventName, Object item)
        {
            switch (eventName)
            {
                case Helper.Event.DELETE_CUSTOMER:

                    for(int i = 0 ; i < customerTabs.Count() ; i++) {
                        if (customerTabs[i].entity == item)
                        {
                            customerTabs.Remove(customerTabs[i]);
                            i--;
                        }
                    }
                    
                    NotifyPropertyChanged("customerTabs");
                    NotifyPropertyChanged("selectedTab");
                    break;
            }
        }
    }
}
