using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrototypeEDUCOM.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PrototypeEDUCOM.View.Customers;
using PrototypeEDUCOM.ViewModel.Customers;
using System.Data.Entity;

namespace PrototypeEDUCOM.ViewModel.Customers
{
    /// <filename>ShowCustomerViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère le contrôle utilisateur qui affiche le détails d'un client</summary>
    class ShowCustomerViewModel : BaseViewModel {

        public Contact customer { get; set; }
        public Phone phone1 { get; set; }
        public Phone phone2 { get; set; }
        public Email email { get; set; }
        public ObservableCollection<Student> students { get; set; }
        public ObservableCollection<Request> ongoingRequests { get; set; }

        public ICommand cmdEditCustomer { get; set; }
        public ICommand cmdDeleteCustomer { get; set; }
        public ICommand cmdAddStudent { get; set; }
        public ICommand cmdEditStudent { get; set; }
        public ICommand cmdDeleteStudent { get; set; }
        public ICommand cmdAddRequest { get; set; }
        public ICommand cmdShowRequest { get; set; }

        /// <summary>
        /// Initialise les valeurs à binder, lie les commandes aux actions et s'abonne au événement le concernant
        /// </summary>
        /// <param name="customer"></param>
        public ShowCustomerViewModel(Contact customer)
        {

            var ongoningRequestQuery = customer.requests.Where(r => r.events.Last().event_types.order != 100);                        
            this.customer = customer;
            this.students = new ObservableCollection<Student>(customer.students.ToList());
            this.ongoingRequests = new ObservableCollection<Request>(ongoningRequestQuery.ToList());
            if (customer.phones.Count > 0)
                this.phone1 = customer.phones.First();
            if (customer.phones.Count > 1)
                this.phone2 = customer.phones.ElementAt(2);
            if (customer.emails.Count > 0)
                this.email = customer.emails.First();
            
            this.cmdEditCustomer = new RelayCommand<Object>(actEditCustomer);
            this.cmdDeleteCustomer = new RelayCommand<Object>(actDeleteCustomer);
            this.cmdAddStudent = new RelayCommand<Object>(actAddStudent);
            this.cmdEditStudent = new RelayCommand<Student>(actEditStudent);
            this.cmdDeleteStudent = new RelayCommand<Student>(actDeleteStudent);
            this.cmdAddRequest = new RelayCommand<Student>(actAddRequest);
            this.cmdShowRequest = new RelayCommand<Request>(actShowRequest);

            mediator.Register(Helper.Event.ADD_STUDENT, this);
            mediator.Register(Helper.Event.DELETE_STUDENT, this);
            mediator.Register(Helper.Event.ADD_REQUEST, this);
            mediator.Register(Helper.Event.DELETE_REQUEST, this);

        }

        /// <summary>
        /// Ouvre la fenêtre d'édition de client
        /// </summary>
        /// <param name="o"></param>
        public void actEditCustomer(object o)
        {
            mediator.openEditCustomerView(this.customer);
        }
        /// <summary>
        /// Ouvre la fenêtre de suppression de client
        /// </summary>
        /// <param name="o"></param>
        public void actDeleteCustomer(object o)
        {
            mediator.openDeleteCustomerView(this.customer);
        }

        /// <summary>
        /// Ouvre la fenêtre d'ajout de client
        /// </summary>
        /// <param name="o"></param>
        public void actAddStudent(object o)
        {
            mediator.openAddStudentView(this.customer);
        }

        /// <summary>
        /// Ouvre la fenêtre d'edition d'étudiant
        /// </summary>
        /// <param name="student">L'étudiant concerné</param>
        public void actEditStudent(Student student)
        {
            mediator.openEditStudentView(student);
        }

        /// <summary>
        /// Ouvre la fenêtre de suppression d'étudiant
        /// </summary>
        /// <param name="student">L'étudiant concerné</param>
        public void actDeleteStudent(Student student)
        {
            mediator.openDeleteStudentView(student);
        }

        /// <summary>
        /// Ouvre la fenêtre d'ajout de demande
        /// </summary>
        /// <param name="o"></param>
        public void actAddRequest(Object o) {
            mediator.openAddRequestView(customer);
        }

        /// <summary>
        /// Ouvre l'onglet de la demande
        /// </summary>
        /// <param name="request">La requête concernée</param>
        public void actShowRequest(Request request) {
            mediator.openShowRequestView(request);
        }

        /// <summary>
        /// Fonction de mise à jour en cas de notification d'événement
        /// </summary>
        /// <param name="eventName">Le type d'événement</param>
        /// <param name="item">l'objet concerné par l'événement</param>
        public override void Update(string eventName, object item)
        {
            switch (eventName)
            {
                case Helper.Event.ADD_STUDENT:
                    this.students.Add((Student)item);
                    NotifyPropertyChanged("students");
                    break;
                case Helper.Event.DELETE_STUDENT:
                    this.students.Remove((Student)item);

                    foreach(Request request in ((Student)item).requests)
                        this.ongoingRequests.Remove(request);

                    NotifyPropertyChanged("ongoingRequests");
                    NotifyPropertyChanged("students");
                    break;
                case Helper.Event.ADD_REQUEST:
                    this.ongoingRequests.Add((Request)item);
                    NotifyPropertyChanged("ongoingRequests");
                    break;
                case Helper.Event.DELETE_REQUEST:
                    this.ongoingRequests.Remove((Request)item);
                    NotifyPropertyChanged("ongoingRequests");
                    break;
            }
        }
    }
}
