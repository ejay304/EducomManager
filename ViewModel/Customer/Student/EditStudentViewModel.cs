using PrototypeEDUCOM.Helper;
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
        public Dictionary<string, string> genders { get { return Dictionaries.genders; } set { } }
        public String gender { get; set; }
        public Dictionary<string, string> kinships { get { return Dictionaries.kinships; } set { } }
        public string kinship { get; set; }
        public Validation validFirstname { get; set; }
        public Validation validLastname { get; set; }
        public Validation validGender { get; set; }
        public Validation validBirthday { get; set; }
        public ICommand cmdEdit { get; set; }
        public Action CloseActionEdit { get; set; }
 
        public EditStudentViewModel(student student)
        {

            this.student = student;
            this.birthday = student.birthday;
            this.gender = student.gender;
            this.firstname = student.firstname;
            this.lastname = student.lastname;
            this.kinship = student.kinship;
            this.cmdEdit = new RelayCommand<object>(actEdit);
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
                student.kinship = this.kinship;
                student.gender = this.gender;

                db.SaveChanges();

                this.CloseActionEdit();
            }
        }
    }
}
