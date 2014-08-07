using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Customers.Students
{
    /// <filename>EditStudentViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère la fenêtre de modification d'un étudiant</summary>
    class EditStudentViewModel : BaseViewModel
    {
        public Student student { get; set; }
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
        public ICommand cmdEdit { get; set; }
        public Action CloseActionEdit { get; set; }
 
        /// <summary>
        /// Initialise les valeur à binder et lie la commande d'édition à l'action
        /// </summary>
        /// <param name="student">L'étudiant à modifier</param>
        public EditStudentViewModel(Student student)
        {

            this.student = student;
            this.birthday = student.birthday;
            this.gender = student.gender;
            this.firstname = student.firstname;
            this.lastname = student.lastname;
            this.kinship = student.kinship;
            this.cmdEdit = new RelayCommand<object>(actEdit);
        }

        /// <summary>
        /// Valide la valeur des champs saisie et modifie l'étudiant
        /// </summary>
        /// <param name="o"></param>
        public void actEdit(object o)
        {

            bool error = false;

            this.validFirstname = new Validation();
            this.validLastname = new Validation();
            this.validGender = new Validation();
          
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

          

            if (!error)
            {
                student.birthday = this.birthday;
                student.kinship = this.kinship;
                student.gender = this.gender;

                db.SaveChanges();

                this.CloseActionEdit();
            }
        }
    }
}
