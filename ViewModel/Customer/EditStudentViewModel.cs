using PrototypeEDUCOM.Helper;
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
        public Validation validFirstname { get; set; }
        public Validation validLastname { get; set; }
        public Validation validGender { get; set; }
        public Validation validBirthday { get; set; }
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

            bool error = false;

            this.validFirstname = new Validation();
            this.validLastname = new Validation();
            this.validGender = new Validation();
            this.validBirthday = new Validation();

            // Validation prénom
            if (!this.firstname.Equals(""))
            {
                student.firstname = this.firstname;
                this.validFirstname.message = "Valide";
                this.validFirstname.valid = true;
                NotifyPropertyChanged("validFirstname");
            }
            else
            {
                this.validFirstname.message = "Champ requis";
                this.validFirstname.valid = false;
                NotifyPropertyChanged("validFirstname");
                error = true;
            }

            // Validation nom
            if (!this.lastname.Equals(""))
            {
                student.lastname = this.lastname;
                this.validLastname.message = "Valide";
                this.validLastname.valid = true;
                NotifyPropertyChanged("validLastname");
            }
            else
            {
                this.validLastname.message = "Champ requis";
                this.validLastname.valid = false;
                NotifyPropertyChanged("validLastname");
                error = true;
            }

            // Validation date de naissance
            if (!this.birthday.Equals(""))
            {
                student.birthday = this.birthday;
                this.validBirthday.message = "Valide";
                this.validBirthday.valid = true;
                NotifyPropertyChanged("validBirthday");
            }
            else
            {
                this.validBirthday.message = "Champ requis";
                this.validBirthday.valid = false;
                NotifyPropertyChanged("validBirthday");
                error = true;
            }

            if (!error)
            {
                student.kinship = kinships.ElementAt(kinshipIndex).getValue();
                student.gender = genders.ElementAt(genderIndex).getValue();

                db.SaveChanges();

                this.CloseActionEdit();
            }
        }
    }
}
