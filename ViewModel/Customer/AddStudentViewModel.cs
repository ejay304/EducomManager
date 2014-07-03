using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Customer
{
    class AddStudentViewModel : BaseViewModel
    {
        public contact customer { get; set; }
        public String firstname { get; set; }
        public DateTime birthday { get; set; }
        public String lastname { get; set; }
        public List<kinship> kinships { get; set; }
        public int kinshipIndex { get; set; }
        public String gender { get; set; }
        public ICommand cmdAdd { get; set; }
        public Action CloseActionAdd { get; set; }
        public ShowCustomerViewModel parentVM { get; set; }


        public AddStudentViewModel(contact customer, ShowCustomerViewModel parentVM) {

            this.customer = customer;
            this.kinships = kinship.list;
            this.cmdAdd = new RelayCommand<object>(actAdd);
            this.parentVM = parentVM;
        }
        public void actAdd(object o)
        { 
            student student = new student();
            student.firstname = firstname;
            student.lastname = lastname;
            student.kinship = kinships.ElementAt(kinshipIndex).getValue();
            student.birthday = birthday;
            student.gender = "man";
            customer.students.Add(student);

            db.SaveChanges();

            parentVM.students.Add(student);
            parentVM.NotifyPropertyChanged("students");

            this.CloseActionAdd();
        }
    }
}
