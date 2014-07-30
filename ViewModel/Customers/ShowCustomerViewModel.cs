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

        public void actEditCustomer(object o)
        {
            mediator.openEditCustomerView(this.customer);
        }
        public void actDeleteCustomer(object o)
        {
            mediator.openDeleteCustomerView(this.customer);
        }

        public void actAddStudent(object o)
        {
            mediator.openAddStudentView(this.customer);
        }

        public void actEditStudent(Student student)
        {
            mediator.openEditStudentView(student);
        }

        public void actDeleteStudent(Student student)
        {
            mediator.openDeleteStudentView(student);
        }

        public void actAddRequest(Object o) {
            mediator.openAddRequestView(customer);
        }

        public void actShowRequest(Request request) {
            mediator.openShowRequestView(request);
        }

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
