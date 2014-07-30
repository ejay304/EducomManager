using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Model;
using PrototypeEDUCOM.View;
using PrototypeEDUCOM.View.Admin;
using PrototypeEDUCOM.View.Customer;
using PrototypeEDUCOM.View.Dashboard;
using PrototypeEDUCOM.View.Organisation;
using PrototypeEDUCOM.View.Organisation.Program.Campus;
using PrototypeEDUCOM.View.Request;
using PrototypeEDUCOM.View.Request.Proposition;
using PrototypeEDUCOM.ViewModel.Admin;
using PrototypeEDUCOM.ViewModel.Customer;
using PrototypeEDUCOM.ViewModel.Dashboard;
using PrototypeEDUCOM.ViewModel.Organisation;
using PrototypeEDUCOM.ViewModel.Organisation.Program.Campus;
using PrototypeEDUCOM.ViewModel.Request;
using PrototypeEDUCOM.ViewModel.Request.Program;
using PrototypeEDUCOM.ViewModel.Request.Proposition;
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

        public Dictionary<string, TabContent> mainTabs = new Dictionary<string, TabContent>();

        private MainViewModel mainViewModel;

        private static MediatorViewModel instance;

        public user user { get; set; }

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

        public void openMainView()
        { 
            mainViewModel = new MainViewModel();
            View.MainView mainView = new View.MainView();
            mainView.DataContext = mainViewModel;

            mainViewModel.CloseWindow = new Action(() => mainView.Close());

            mainView.Show();
        }

        public void openLoginView()
        {
            container.Clear();
            mainTabs.Clear();
            new LoginView().Show();
        }

        #region Customer 

        public UserControl openListCustomerView()
        {
            ListCustomerUCView view = new ListCustomerUCView();
            view.DataContext = new ListCustomerViewModel();

            return view;
        }

        public void openShowCustomerView(contact customer)
        {
            ShowCustomerUCView showCustomerView = new ShowCustomerUCView();
            showCustomerView.DataContext = new ShowCustomerViewModel(customer);

            ((CustomerViewModel)mainTabs["customer"].tabViewModel).actAddTab(customer, showCustomerView);

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
            editCustomerViewModel.CloseActionFormEdit = new Action(() => editCustomerView.Close());

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
        #endregion

        #region Organisation

        public UserControl openListOrganisationView()
        {
            ListOrganisationUCView view = new ListOrganisationUCView();
            view.DataContext = new ListOrganisationViewModel();

            return view;
        }

        public void openShowOrganisationView(organisation organisation)
        {
            ShowOrganisationUCView showOrganisationView = new ShowOrganisationUCView();
            showOrganisationView.DataContext = new ShowOrganisationViewModel(organisation);

            ((OrganisationViewModel)mainTabs["organisation"].tabViewModel).actAddTab(organisation, showOrganisationView);

        }

        public void openAddOrganisationView()
        {
            AddOrganisationViewModel addOrganisationViewModel = new AddOrganisationViewModel();
            AddOrganisationView addOrganisationView = new AddOrganisationView();

            addOrganisationView.DataContext = addOrganisationViewModel;
            addOrganisationViewModel.CloseActionFormAdd = new Action(() => addOrganisationView.Close());

            addOrganisationView.Show(); 
        }

        public void openEditOrganisationView(organisation organisation)
        {
            EditOrganisationViewModel editOrganisationViewModel = new EditOrganisationViewModel(organisation);
            EditOrganisationView editOrganisationView = new EditOrganisationView();

            editOrganisationView.DataContext = editOrganisationViewModel;
            editOrganisationViewModel.CloseActionFormEdit = new Action(() => editOrganisationView.Close());

            editOrganisationView.Show();
        }

        public void openDeleteOrganisationView(organisation organisation)
        {
            DeleteOrganisationViewModel deleteOrganisationViewModel = new DeleteOrganisationViewModel(organisation);
            DeleteOrganisationView deleteOrganisationView = new DeleteOrganisationView();

            deleteOrganisationView.DataContext = deleteOrganisationViewModel;
            deleteOrganisationViewModel.CloseActionDelete = new Action(() => deleteOrganisationView.Close());

            deleteOrganisationView.ShowDialog();
        }
 
        #endregion

        #region Program

        public void openShowProgramView(program program)
        {
            ShowProgramUCView showProgramView = new ShowProgramUCView();
            showProgramView.DataContext = new ShowProgramViewModel(program);

            ((OrganisationViewModel)mainTabs["organisation"].tabViewModel).actAddTab(program, showProgramView);

        }

        public void openAddProgramView(organisation organisation)
        {
            AddProgramViewModel addProgramViewModel = new AddProgramViewModel(organisation);
            AddProgramView addProgramView = new AddProgramView();

            addProgramView.DataContext = addProgramViewModel;
            addProgramViewModel.CloseActionAdd = new Action(() => addProgramView.Close());

            addProgramView.Show();
        }

        public void openEditProgramView(program program)
        {
            EditProgramViewModel editProgramViewModel = new EditProgramViewModel(program);
            EditProgramView editProgramView = new EditProgramView();

            editProgramView.DataContext = editProgramViewModel;
            editProgramViewModel.CloseActionEdit = new Action(() => editProgramView.Close());

            editProgramView.Show();
   
        }

        public void openDeleteProgramView(program program)
        {
            DeleteProgramViewModel deleteProgramViewModel = new DeleteProgramViewModel(program);
            DeleteProgramView deleteProgramView = new DeleteProgramView();

            deleteProgramView.DataContext = deleteProgramViewModel;
            deleteProgramViewModel.CloseActionDelete = new Action(() => deleteProgramView.Close());

            deleteProgramView.ShowDialog();
        }

        #endregion

        #region Request

        public UserControl openListRequestView()
        {
            ListRequestUCView view = new ListRequestUCView();
            view.DataContext = new ListRequestViewModel();

            return view;
        }

        public void openShowRequestView(request request)
        {
            mainViewModel.selectedTab = BaseViewModel.tabs["request"];
            ShowRequestUCView showRequestView = new ShowRequestUCView();
            showRequestView.DataContext = new ShowRequestViewModel(request);

            ((RequestViewModel)mainTabs["request"].tabViewModel).actAddTab(request, showRequestView);
        }

        public void openAddRequestView(contact customer)
        {
            AddRequestViewModel addRequestViewModel = new AddRequestViewModel(customer);
            AddRequestView addRequestView = new AddRequestView();

            addRequestView.DataContext = addRequestViewModel;
            addRequestViewModel.CloseActionAdd = new Action(() => addRequestView.Close());

            addRequestView.Show();
        }
        public void openDeleteRequestView(request request) {
            DeleteRequestViewModel deleteRequestViewModel = new DeleteRequestViewModel(request);
            DeleteRequestView deleteRequestView = new DeleteRequestView();

            deleteRequestView.DataContext = deleteRequestViewModel;
            deleteRequestViewModel.CloseActionDelete = new Action(() => deleteRequestView.Close());

            deleteRequestView.ShowDialog();
        }

        public void openEditRequestView(request request)
        {
            EditRequestViewModel editRequestViewModel = new EditRequestViewModel(request);
            EditRequestView editRequestView = new EditRequestView();

            editRequestView.DataContext = editRequestViewModel;
            editRequestViewModel.CloseActionEdit = new Action(() => editRequestView.Close());

            editRequestView.Show();
        }

        #endregion

        #region Campus

        public void openAddCampusView(program program)
        {
            AddCampusViewModel addCampusViewModel = new AddCampusViewModel(program);
            AddCampusView addCampusView = new AddCampusView();

            addCampusView.DataContext = addCampusViewModel;
            addCampusViewModel.CloseActionAdd = new Action(() => addCampusView.Close());

            addCampusView.Show();
        }

        public void openEditCampusView(campu campus)
        {
            EditCampusViewModel editCampusViewModel = new EditCampusViewModel(campus);
            EditCampusView editCampusView = new EditCampusView();

            editCampusView.DataContext = editCampusViewModel;
            editCampusViewModel.CloseActionAdd = new Action(() => editCampusView.Close());

            editCampusView.Show();
        }

        public void openDeleteCampusView(campu campus) {
            DeleteCampusViewModel deleteCampusViewModel = new DeleteCampusViewModel(campus);
            DeleteCampusView deleteCampusView = new DeleteCampusView();

            deleteCampusView.DataContext = deleteCampusViewModel;
            deleteCampusViewModel.CloseActionDelete = new Action(() => deleteCampusView.Close());

            deleteCampusView.ShowDialog();   
        }

        #endregion




        public void openAddPropositionView(request request){
            AddPropositionViewModel addPropositionViewModel = new AddPropositionViewModel(request);
            AddPropositionView addPropositionView = new AddPropositionView();

            addPropositionView.DataContext = addPropositionViewModel;
            addPropositionViewModel.CloseActionAdd = new Action(() => addPropositionView.Close());

            addPropositionView.Show(); 
        }


        public void openInscriptionView(proposition proposition)
        {
            AddInscriptionViewModel addInscriptionViewModel = new AddInscriptionViewModel(proposition);
            AddInscriptionView addInscriptionView = new AddInscriptionView();

            addInscriptionView.DataContext = addInscriptionViewModel;
            addInscriptionViewModel.CloseActionAdd = new Action(() => addInscriptionView.Close());

            addInscriptionView.Show(); 
        }

        public void createTabViewModel()
        {
            if (!user.role.Equals("assistant"))
            {
                // Onglet dashboard
                DashboardViewModel dashboardViewModel = new DashboardViewModel();
                DashboardUCView dashboardUCView =  new DashboardUCView();

                dashboardUCView.DataContext = dashboardViewModel;

                mainTabs.Add(TabName.DASHBORAD, new TabContent(dashboardViewModel, dashboardUCView));
            }

            if (user.role.Equals("administrator"))
            {
                // Onglet admin
                AdminViewModel adminViewModel = new AdminViewModel();
                AdminUCView adminUCView = new AdminUCView();

                adminUCView.DataContext = adminViewModel;

                mainTabs.Add(TabName.ADMIN, new TabContent(adminViewModel, adminUCView));
            }

            // Onglet client
            CustomerViewModel customerViewModel = new CustomerViewModel();
            CustomerUCView customerUCView = new CustomerUCView();

            customerUCView.DataContext = customerViewModel;

            mainTabs.Add(TabName.CUSTOMER, new TabContent(customerViewModel, customerUCView));

            // Onglet organisation
            OrganisationViewModel organisationViewModel = new OrganisationViewModel();
            OrganisationUCView organisationUCView = new OrganisationUCView();

            organisationUCView.DataContext = organisationViewModel;

            mainTabs.Add(TabName.ORGANISATION, new TabContent(organisationViewModel, organisationUCView));

            // Onglet demande
            RequestViewModel requestViewModel = new RequestViewModel();
            RequestUCView requestUCView = new RequestUCView();

            requestUCView.DataContext = requestViewModel;

            mainTabs.Add(TabName.REQUEST, new TabContent(requestViewModel, requestUCView));
        }

        public void openAddStudentView(contact customer) {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(customer);
            AddStudentView addStudentView = new AddStudentView();

            addStudentView.DataContext = addStudentViewModel;
            addStudentViewModel.CloseActionAdd = new Action(() => addStudentView.Close());

            addStudentView.Show();
        }
        public void openEditStudentView(student student) {
            EditStudentViewModel editStudentViewModel = new EditStudentViewModel(student);
            EditStudentView editStudentView = new EditStudentView();

            editStudentView.DataContext = editStudentViewModel;
            editStudentViewModel.CloseActionEdit = new Action(() => editStudentView.Close());

            editStudentView.Show();
       
        }
        public void openDeleteStudentView(student student) {

            DeleteStudentViewModel deleteStudentViewModel = new DeleteStudentViewModel(student);
            DeleteStudentView deleteStudentView = new DeleteStudentView();

            deleteStudentView.DataContext = deleteStudentViewModel;
            deleteStudentViewModel.CloseActionDelete = new Action(() => deleteStudentView.Close());

            deleteStudentView.ShowDialog();
        }


    }
}
