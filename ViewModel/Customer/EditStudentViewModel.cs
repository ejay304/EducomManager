using PrototypeEDUCOM.Helper.Enum;
using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Customer
{
    class EditStudentViewModel : BaseViewModel
    {
        public student student { get; set; }
        public String firstname { get; set; }
        public DateTime birthday { get; set; }
        public String lastname { get; set; }
        public List<Gender> genders { get; set; }
        public int genderIndex { get; set; }
        public List<Kinship> kinships { get; set; }
        public int kinshipIndex { get; set; }
        public String gender { get; set; }
        public ICommand cmdAdd { get; set; }
        public Action CloseActionEdit { get; set; }
        public ShowCustomerViewModel parentVM { get; set; }

        public EditStudentViewModel(student student, ShowCustomerViewModel parentVM)
        {

            this.student = student;
            this.birthday = student.birthday;
            this.genderIndex = Gender.indexByValue(student.gender);
            this.firstname = student.firstname;
            this.lastname = student.lastname;
            this.kinshipIndex = Kinship.indexByValue(student.kinship);
            this.kinships = Kinship.list;
            this.genders = Gender.list;
            this.cmdAdd = new RelayCommand<object>(actEdit);
            this.parentVM = parentVM;
        }
        public void actEdit(object o)
        { 
            student.firstname = firstname;
            student.lastname = lastname;
            student.kinship = kinships.ElementAt(kinshipIndex).getValue();
            student.birthday = birthday;
            student.gender = genders.ElementAt(genderIndex).getValue();
            
            db.SaveChanges();

            parentVM.NotifyPropertyChanged("students.firstname");

            this.CloseActionEdit();
        }
    }
}
