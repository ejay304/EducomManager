using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Model;
using PrototypeEDUCOM.View;
using PrototypeEDUCOM.View.Admin;
using PrototypeEDUCOM.View.Customers;
using PrototypeEDUCOM.View.Customers.Students;
using PrototypeEDUCOM.View.Dashboard;
using PrototypeEDUCOM.View.Organisations;
using PrototypeEDUCOM.View.Organisations.Programs;
using PrototypeEDUCOM.View.Organisations.Programs.Campuses;
using PrototypeEDUCOM.View.Requests;
using PrototypeEDUCOM.View.Requests.Propositions;
using PrototypeEDUCOM.ViewModel.Admin;
using PrototypeEDUCOM.ViewModel.Customers;
using PrototypeEDUCOM.ViewModel.Customers.Students;
using PrototypeEDUCOM.ViewModel.Dashboard;
using PrototypeEDUCOM.ViewModel.Organisations;
using PrototypeEDUCOM.ViewModel.Organisations.Programs;
using PrototypeEDUCOM.ViewModel.Organisations.Programs.Campuses;
using PrototypeEDUCOM.ViewModel.Requests;
using PrototypeEDUCOM.ViewModel.Requests.Propositions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PrototypeEDUCOM.ViewModel

{
    /// <filename>MediatorViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Imlémentation du médiateur servant a communiquer entre les ViewModels</summary>
    public class MediatorViewModel  
    {

        private Dictionary<string, List<BaseViewModel>> container = new Dictionary<string, List<BaseViewModel>>();

        public Dictionary<string, TabContent> mainTabs = new Dictionary<string, TabContent>();

        /// <summary>
        /// L'utilisateur authentifié
        /// </summary>
        public User user { get; set; }

        private MainViewModel mainViewModel;

        private static MediatorViewModel instance;

        /// <summary>
        /// Retourne l'unique instance du médiateur, dans le cas où elle n'existe pas il va l'instancier.
        /// </summary>
        /// <returns>Référence sur le médiateur</returns>
        public static MediatorViewModel getInstance()
        {
            if (instance == null)
                instance = new MediatorViewModel();
            return instance;
        }

        /// <summary>
        /// Permet d'enregistrer un Observer à un événement
        /// </summary>
        /// <param name="eventName">Le nom de l'événement</param>
        /// <param name="vm">L'observer</param>
        public void Register(string eventName, BaseViewModel vm)
        {
            if (!container.ContainsKey(eventName))
                container[eventName] = new List<BaseViewModel>();

            container[eventName].Add(vm);
        }

        /// <summary>
        ///  Permet de notifier les observers d'un événement
        /// </summary>
        /// <param name="eventName">Le type d'événement</param>
        /// <param name="item">L'objet concerné par la notofocation</param>
        public void NotifyViewModel(string eventName, object item)
        {
            if (container.ContainsKey(eventName))
                foreach (BaseViewModel vm in container[eventName])
                    vm.Update(eventName, item);
        }

        /// <summary>
        /// Ouvre la fenêtre principale de l'application.
        /// </summary>
        public void openMainView()
        { 
            mainViewModel = new MainViewModel();
            View.MainView mainView = new View.MainView();
            mainView.DataContext = mainViewModel;

            mainViewModel.CloseWindow = new Action(() => mainView.Close());

            mainView.Show();
        }

        /// <summary>
        /// Initialise les onglets de premier niveau en fonction de l'utilisateur authentifié
        /// </summary>
        public void createTabViewModel()
        {
            if (!user.role.Equals("assistant"))
            {
                // Onglet dashboard
                DashboardViewModel dashboardViewModel = new DashboardViewModel();
                DashboardUCView dashboardUCView = new DashboardUCView();

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

        /// <summary>
        /// Ouvre la fenêtre de login
        /// </summary>
        public void openLoginView()
        {
            container.Clear();
            mainTabs.Clear();
            new LoginView().Show();
        }

        #region Customer 

        /// <summary>
        /// Ajoute l'onglet avec la liste des clients
        /// </summary>
        /// <returns>Le UserControl créé</returns>
        public UserControl openListCustomerView()
        {
            ListCustomerUCView view = new ListCustomerUCView();
            view.DataContext = new ListCustomerViewModel();

            return view;
        }

        /// <summary>
        /// Ouvre l'onglet du détails d'un client
        /// </summary>
        /// <param name="customer">le client à afficher</param>
        public void openShowCustomerView(Contact customer)
        {
            ShowCustomerUCView showCustomerView = new ShowCustomerUCView();
            showCustomerView.DataContext = new ShowCustomerViewModel(customer);

            ((CustomerViewModel)mainTabs["customer"].tabViewModel).actAddTab(customer, showCustomerView);

        }

        /// <summary>
        /// Ouvre la fenêtre d'ajout d'un utilisateur
        /// </summary>
        public void openAddCustomerView()
        {
            AddCustomerViewModel addCustomerViewModel = new AddCustomerViewModel();
            AddCustomerView addCustomerView = new AddCustomerView();

            addCustomerView.DataContext = addCustomerViewModel;
            addCustomerViewModel.CloseActionAdd = new Action(() => addCustomerView.Close());

            addCustomerView.Show();
        }

        /// <summary>
        ///  Ouvre la fenêtre de modification d'un utilisateur
        /// </summary>
        /// <param name="customer">Le client à modifier</param>
        public void openEditCustomerView(Contact customer)
        {
            EditCustomerViewModel editCustomerViewModel = new EditCustomerViewModel(customer);
            EditCustomerView editCustomerView = new EditCustomerView();

            editCustomerView.DataContext = editCustomerViewModel;
            editCustomerViewModel.CloseActionEdit = new Action(() => editCustomerView.Close());

            editCustomerView.Show();
        }
        
        /// <summary>
        /// Ouvre la fenêtre de suppresion de client 
        /// </summary>
        /// <param name="customer">Le client à supprimer</param>
        public void openDeleteCustomerView(Contact customer)
        {
            DeleteCustomerViewModel deleteCustomerViewModel = new DeleteCustomerViewModel(customer);
            DeleteCustomerView deleteCustomerView = new DeleteCustomerView();

            deleteCustomerView.DataContext = deleteCustomerViewModel;
            deleteCustomerViewModel.CloseActionDelete = new Action(() => deleteCustomerView.Close());

            deleteCustomerView.ShowDialog();
        }
        #endregion

        #region Student

        public void openAddStudentView(Contact customer)
        {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(customer);
            AddStudentView addStudentView = new AddStudentView();

            addStudentView.DataContext = addStudentViewModel;
            addStudentViewModel.CloseActionAdd = new Action(() => addStudentView.Close());

            addStudentView.Show();
        }
        public void openEditStudentView(Student student)
        {
            EditStudentViewModel editStudentViewModel = new EditStudentViewModel(student);
            EditStudentView editStudentView = new EditStudentView();

            editStudentView.DataContext = editStudentViewModel;
            editStudentViewModel.CloseActionEdit = new Action(() => editStudentView.Close());

            editStudentView.Show();

        }
        public void openDeleteStudentView(Student student)
        {

            DeleteStudentViewModel deleteStudentViewModel = new DeleteStudentViewModel(student);
            DeleteStudentView deleteStudentView = new DeleteStudentView();

            deleteStudentView.DataContext = deleteStudentViewModel;
            deleteStudentViewModel.CloseActionDelete = new Action(() => deleteStudentView.Close());

            deleteStudentView.ShowDialog();
        }

        #endregion

        #region Organisation

        /// <summary>
        /// Ajoute l'onglet avec la liste des organisations
        /// </summary>
        /// <returns>Le UserControl créé</returns>
        public UserControl openListOrganisationView()
        {
            ListOrganisationUCView view = new ListOrganisationUCView();
            view.DataContext = new ListOrganisationViewModel();

            return view;
        }

        /// <summary>
        /// Ouvre l'onglet du détails d'une organisation
        /// </summary>
        /// <param name="organisation">l'organisation à afficher</param>
        public void openShowOrganisationView(Organisation organisation)
        {
            ShowOrganisationUCView showOrganisationView = new ShowOrganisationUCView();
            showOrganisationView.DataContext = new ShowOrganisationViewModel(organisation);

            ((OrganisationViewModel)mainTabs["organisation"].tabViewModel).actAddTab(organisation, showOrganisationView);

        }

        /// <summary>
        /// Ouvre la fenêtre d'ajout d'une organisation
        /// </summary>
        public void openAddOrganisationView()
        {
            AddOrganisationViewModel addOrganisationViewModel = new AddOrganisationViewModel();
            AddOrganisationView addOrganisationView = new AddOrganisationView();

            addOrganisationView.DataContext = addOrganisationViewModel;
            addOrganisationViewModel.CloseActionAdd = new Action(() => addOrganisationView.Close());

            addOrganisationView.Show(); 
        }

        /// <summary>
        ///  Ouvre la fenêtre de modification d'une organisaiton
        /// </summary>
        /// <param name="organisation">L'organisation à modifier</param>
        public void openEditOrganisationView(Organisation organisation)
        {
            EditOrganisationViewModel editOrganisationViewModel = new EditOrganisationViewModel(organisation);
            EditOrganisationView editOrganisationView = new EditOrganisationView();

            editOrganisationView.DataContext = editOrganisationViewModel;
            editOrganisationViewModel.CloseActionEdit = new Action(() => editOrganisationView.Close());

            editOrganisationView.Show();
        }

        /// <summary>
        /// Ouvre la fenêtre de suppresion d'organisation 
        /// </summary>
        /// <param name="organisation">L'organisation à supprimer</param>
        public void openDeleteOrganisationView(Organisation organisation)
        {
            DeleteOrganisationViewModel deleteOrganisationViewModel = new DeleteOrganisationViewModel(organisation);
            DeleteOrganisationView deleteOrganisationView = new DeleteOrganisationView();

            deleteOrganisationView.DataContext = deleteOrganisationViewModel;
            deleteOrganisationViewModel.CloseActionDelete = new Action(() => deleteOrganisationView.Close());

            deleteOrganisationView.ShowDialog();
        }
 
        #endregion

        #region Program

        /// <summary>
        /// Ouvre l'onglet du détails d'un programme
        /// </summary>
        /// <param name="program">le programme à afficher</param
        public void openShowProgramView(Program program)
        {
            ShowProgramUCView showProgramView = new ShowProgramUCView();
            showProgramView.DataContext = new ShowProgramViewModel(program);

            ((OrganisationViewModel)mainTabs["organisation"].tabViewModel).actAddTab(program, showProgramView);

        }

        /// <summary>
        ///  Ouvre la fenêtre d'ajout d'un programme
        /// </summary>
        /// <param name="organisation">L'organisation a laquelle le programme appartient</param>
        public void openAddProgramView(Organisation organisation)
        {
            AddProgramViewModel addProgramViewModel = new AddProgramViewModel(organisation);
            AddProgramView addProgramView = new AddProgramView();

            addProgramView.DataContext = addProgramViewModel;
            addProgramViewModel.CloseActionAdd = new Action(() => addProgramView.Close());

            addProgramView.Show();
        }

        /// <summary>
        ///  Ouvre la fenêtre de modification d'un programme
        /// </summary>
        /// <param name="program">Le programme à modifier</param>
        public void openEditProgramView(Program program)
        {
            EditProgramViewModel editProgramViewModel = new EditProgramViewModel(program);
            EditProgramView editProgramView = new EditProgramView();

            editProgramView.DataContext = editProgramViewModel;
            editProgramViewModel.CloseActionEdit = new Action(() => editProgramView.Close());

            editProgramView.Show();
   
        }

        /// <summary>
        /// Ouvre la fenêtre de suppresion d'un programme 
        /// </summary>
        /// <param name="program">Le programme à supprimer</param>
        public void openDeleteProgramView(Program program)
        {
            DeleteProgramViewModel deleteProgramViewModel = new DeleteProgramViewModel(program);
            DeleteProgramView deleteProgramView = new DeleteProgramView();

            deleteProgramView.DataContext = deleteProgramViewModel;
            deleteProgramViewModel.CloseActionDelete = new Action(() => deleteProgramView.Close());

            deleteProgramView.ShowDialog();
        }

        #endregion

        #region Campus

        /// <summary>
        /// Ouvre la fenêtre d'ajout d'un campus
        /// </summary>
        /// <param name="program"></param>
        public void openAddCampusView(Program program)
        {
            AddCampusViewModel addCampusViewModel = new AddCampusViewModel(program);
            AddCampusView addCampusView = new AddCampusView();

            addCampusView.DataContext = addCampusViewModel;
            addCampusViewModel.CloseActionAdd = new Action(() => addCampusView.Close());

            addCampusView.Show();
        }

        /// <summary>
        /// Ouvre la fenêtre de modificaiton d'un campus
        /// </summary>
        /// <param name="campus">Le campus à modifer</param>
        public void openEditCampusView(Campus campus)
        {
            EditCampusViewModel editCampusViewModel = new EditCampusViewModel(campus);
            EditCampusView editCampusView = new EditCampusView();

            editCampusView.DataContext = editCampusViewModel;
            editCampusViewModel.CloseActionAdd = new Action(() => editCampusView.Close());

            editCampusView.Show();
        }

        /// <summary>
        /// Ouvre la fenêtre de suppression d'un campus
        /// </summary>
        /// <param name="campus">Le campus à supprimer</param>
        public void openDeleteCampusView(Campus campus)
        {
            DeleteCampusViewModel deleteCampusViewModel = new DeleteCampusViewModel(campus);
            DeleteCampusView deleteCampusView = new DeleteCampusView();

            deleteCampusView.DataContext = deleteCampusViewModel;
            deleteCampusViewModel.CloseActionDelete = new Action(() => deleteCampusView.Close());

            deleteCampusView.ShowDialog();
        }

        #endregion

        #region Request

        /// <summary>
        /// Ouvre l'onglet contenant la liste des demandes
        /// </summary>
        /// <returns>Le UserControl créé</returns>
        public UserControl openListRequestView()
        {
            ListRequestUCView view = new ListRequestUCView();
            view.DataContext = new ListRequestViewModel();

            return view;
        }

        /// <summary>
        /// Ouvre l'onglet contenant le détail d'une demande
        /// </summary>
        /// <param name="request">La demande concernée</param>
        public void openShowRequestView(Request request)
        {
            mainViewModel.selectedTab = BaseViewModel.tabs["request"];
            ShowRequestUCView showRequestView = new ShowRequestUCView();
            showRequestView.DataContext = new ShowRequestViewModel(request);

            ((RequestViewModel)mainTabs["request"].tabViewModel).actAddTab(request, showRequestView);
        }

        /// <summary>
        /// Ouvre la fenêtre contenant la liste d'événement d'une demande
        /// </summary>
        /// <param name="request">la demande concernée</param>
        public void openListEventView(Request request)
        {
            ListEventViewModel listEventViewModel = new ListEventViewModel(request);
            ListEventView listEventView = new ListEventView();

            listEventView.DataContext = listEventViewModel;

            listEventView.Show();
        }

        /// <summary>
        /// Ouvre la fenêtre d'ajout d'une demande
        /// </summary>
        /// <param name="customer">le client lié</param>
        public void openAddRequestView(Contact customer)
        {
            AddRequestViewModel addRequestViewModel = new AddRequestViewModel(customer);
            AddRequestView addRequestView = new AddRequestView();

            addRequestView.DataContext = addRequestViewModel;
            addRequestViewModel.CloseActionAdd = new Action(() => addRequestView.Close());

            addRequestView.Show();
        }

        /// <summary>
        /// Ouvre la fenêtre de modificaiton de demande
        /// </summary>
        /// <param name="request">La demande à modifiers</param>
        public void openEditRequestView(Request request)
        {
            EditRequestViewModel editRequestViewModel = new EditRequestViewModel(request);
            EditRequestView editRequestView = new EditRequestView();

            editRequestView.DataContext = editRequestViewModel;
            editRequestViewModel.CloseActionEdit = new Action(() => editRequestView.Close());

            editRequestView.Show();
        }

        /// <summary>
        /// Ouvre la fenêtre de suppression de demande
        /// </summary>
        /// <param name="request">La requête à supprimer</param>
        public void openDeleteRequestView(Request request) {
            DeleteRequestViewModel deleteRequestViewModel = new DeleteRequestViewModel(request);
            DeleteRequestView deleteRequestView = new DeleteRequestView();

            deleteRequestView.DataContext = deleteRequestViewModel;
            deleteRequestViewModel.CloseActionDelete = new Action(() => deleteRequestView.Close());

            deleteRequestView.ShowDialog();
        }

        #endregion

        #region Event

        public void openAddPropositionView(Request request){
            AddPropositionViewModel addPropositionViewModel = new AddPropositionViewModel(request);
            AddPropositionView addPropositionView = new AddPropositionView();

            addPropositionView.DataContext = addPropositionViewModel;
            addPropositionViewModel.CloseActionAdd = new Action(() => addPropositionView.Close());

            addPropositionView.Show(); 
        }


        public void openInscriptionView(Proposition proposition)
        {
            AddInscriptionViewModel addInscriptionViewModel = new AddInscriptionViewModel(proposition);
            AddInscriptionView addInscriptionView = new AddInscriptionView();

            addInscriptionView.DataContext = addInscriptionViewModel;
            addInscriptionViewModel.CloseActionAdd = new Action(() => addInscriptionView.Close());

            addInscriptionView.Show(); 
        }

        #endregion


    }
}
