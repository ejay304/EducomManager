using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrototypeEDUCOM.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PrototypeEDUCOM.View.Customer;
using PrototypeEDUCOM.ViewModel.Customer;
namespace PrototypeEDUCOM.ViewModel.Customer
{
    class ShowCustomerViewModel : BaseViewModel {

        public contact customer { get; set; }
        public ObservableCollection<student> students { get; set; }

        public ICommand cmdEditCustomer { get; set; }
        public ICommand cmdDeleteCustomer { get; set; }
        public ICommand cmdAddStudent { get; set; }
        public ICommand cmdEditStudent { get; set; }
        public ICommand cmdDeleteStudent { get; set; }

        public ShowCustomerViewModel(contact customer)
        {
            this.customer = customer;
            this.students = new ObservableCollection<student>(customer.students.ToList());
            this.cmdEditCustomer = new RelayCommand<Object>(actEditCustomer);
            this.cmdDeleteCustomer = new RelayCommand<Object>(actDeleteCustomer);
            this.cmdAddStudent = new RelayCommand<Object>(actAddStudent);
            this.cmdEditStudent = new RelayCommand<student>(actEditStudent);
            this.cmdDeleteStudent = new RelayCommand<student>(actDeleteStudent);

            mediator.Register(Helper.Event.ADD_STUDENT, this);
            mediator.Register(Helper.Event.DELETE_STUDENT, this);
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
        public void actEditStudent(student student)
        {
            mediator.openEditStudentView(student);
        }
        public void actDeleteStudent(student student)
        {
            mediator.openDeleteStudentView(student);
        }

        public override void Update(string eventName, object item)
        {
            switch (eventName)
            {
                case Helper.Event.ADD_STUDENT:

                    this.students.Add((student)item);
                    NotifyPropertyChanged("students");
                    break;
                case Helper.Event.DELETE_STUDENT:

                    // Ajoute dans la liste
                    this.students.Remove((student)item);
                    NotifyPropertyChanged("students");
                    break;
            }
        }
    }
}
