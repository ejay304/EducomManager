using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Model;
using PrototypeEDUCOM.View.Customer;
using PrototypeEDUCOM.View.Dashboard;
using PrototypeEDUCOM.View.Organisation;
using PrototypeEDUCOM.ViewModel.Customer;
using PrototypeEDUCOM.ViewModel.Dashboard;
using PrototypeEDUCOM.ViewModel.Organisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PrototypeEDUCOM.ViewModel
{

    public class MediatorViewModel
    {

        private Dictionary<string, List<BaseViewModel>> container = new Dictionary<string, List<BaseViewModel>>();

        public Dictionary<string, BaseViewModel> TabViewModel = new Dictionary<string,BaseViewModel>();
        public Dictionary<string, UserControl> TabUC = new Dictionary<string, UserControl>();

        private static MediatorViewModel instance;

        public Helper.Enum.User roleUser { get; set; }

        public static MediatorViewModel getInstance()
        {
            if (instance == null)
                instance = new MediatorViewModel();
            return instance;
        }


        public void Register(string eventName, BaseViewModel vm)
        {
            if (!container.ContainsKey(eventName))
                container[eventName] = new List<BaseViewModel>();

            container[eventName].Add(vm);
        }

        public void NotifyViewModel(string eventName, object item)
        {
            if (container.ContainsKey(eventName))
                foreach (BaseViewModel vm in container[eventName])
                    vm.Update(eventName, item);
        }

        public UserControl openListCustomerView()
        {
            ListCustomerUCView view = new ListCustomerUCView();
            view.DataContext = new ListCustomerViewModel();

            return view;
        }

        public void openAddCustomerView()
        {
            AddCustomerViewModel addCustomerViewModel = new AddCustomerViewModel();
            AddCustomerView addCustomerView = new AddCustomerView();

            addCustomerView.DataContext = addCustomerViewModel;
            addCustomerViewModel.CloseActionFormAdd = new Action(() => addCustomerView.Close());

            addCustomerView.Show();
        }

        public void openEditCustomerView(contact customer)
        {
            EditCustomerViewModel editCustomerViewModel = new EditCustomerViewModel(customer);
            EditCustomerView editCustomerView = new EditCustomerView();

            editCustomerView.DataContext = editCustomerViewModel;
            editCustomerViewModel.CloseActionFormAdd = new Action(() => editCustomerView.Close());

            editCustomerView.Show();
        }

        public void openDeleteCustomerView(contact customer)
        {
            DeleteCustomerViewModel deleteCustomerViewModel = new DeleteCustomerViewModel(customer);
            DeleteCustomerView deleteCustomerView = new DeleteCustomerView();

            deleteCustomerView.DataContext = deleteCustomerViewModel;
            deleteCustomerViewModel.CloseActionDelete = new Action(() => deleteCustomerView.Close());

            deleteCustomerView.ShowDialog();
        }

        public void openShowCustomerView(contact customer)
        {
            ShowCustomerUCView showCustomerView = new ShowCustomerUCView();
            showCustomerView.DataContext = new ShowCustomerViewModel(customer);

            ((CustomerViewModel)TabViewModel["customer"]).actAddTab(customer, showCustomerView);

        }

        public UserControl openListOrganisationView()
        {
            ListOrganisationUCView view = new ListOrganisationUCView();
            view.DataContext = new ListOrganisationViewModel();

            return view;
        }

        public void openAddOrganisationView()
        {
            AddOrganisationViewModel addOrganisationViewModel = new AddOrganisationViewModel();
            AddOrganisationView addOrganisationView = new AddOrganisationView();

            addOrganisationView.DataContext = addOrganisationViewModel;
            addOrganisationViewModel.CloseActionFormAdd = new Action(() => addOrganisationView.Close());

            addOrganisationView.Show(); 
        }

        public void openShowOrganisationView(organisation organisation)
        {
            ShowOrganisationUCView showOrganisationView = new ShowOrganisationUCView();
            showOrganisationView.DataContext = new ShowOrganisationViewModel(organisation);

            ((OrganisationViewModel)TabViewModel["organisation"]).actAddTab(organisation, showOrganisationView);

        }

        public void createTabViewModel()
        {
            if (this.roleUser != Helper.Enum.User.assistant)
            {
                // Onglet dashboard
                DashboardViewModel dashboardViewModel = new DashboardViewModel();
                DashboardUCView dashboardUCView =  new DashboardUCView();

                dashboardUCView.DataContext = dashboardViewModel;

                TabViewModel.Add("dashboard", dashboardViewModel);
                TabUC.Add("dashboard",dashboardUCView);

            }

            // Onglet client
            CustomerViewModel customerViewModel = new CustomerViewModel();
            CustomerUCView customerUCView = new CustomerUCView();

            customerUCView.DataContext = customerViewModel;

            TabViewModel.Add("customer", customerViewModel);
            TabUC.Add("customer", customerUCView);

            // Onglet organisation
            OrganisationViewModel organisationViewModel = new OrganisationViewModel();
            OrganisationUCView organisationUCView = new OrganisationUCView();

            organisationUCView.DataContext = organisationViewModel;

            TabViewModel.Add("organisation", organisationViewModel);
            TabUC.Add("organisation", organisationUCView);
        }
    }
}
