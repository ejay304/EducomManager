
﻿using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Customers.Students
{
    /// <filename>AddStudentViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère la fenêtre d'ajout d'étudiants</summary>
    class AddStudentViewModel : BaseViewModel
    {
        public Contact customer { get; set; }
        public String firstname { get; set; }
        public DateTime birthday { get; set; }
        public String lastname { get; set; }
        public Dictionary<string, string> genders { get { return Dictionaries.genders; } set { } }
        public string gender { get; set; }
        public Dictionary<string, string> kinships { get { return Dictionaries.kinships; } set { } }
        public string kinship { get; set; }
        public Validation validFirstname { get; set; }
        public Validation validLastname { get; set; }
        public Validation validGender { get; set; }
        public ICommand cmdAdd { get; set; }
        public Action CloseActionAdd { get; set; }
    
        /// <summary>
        /// Initialise les valeurs à binder et lie la commande d'ajout à l'action
        /// </summary>
        /// <param name="customer"></param>
        public AddStudentViewModel(Contact customer) {

            this.customer = customer;
            this.birthday = DateTime.Now;
            this.kinship = kinships.First().Key;
            this.gender = genders.First().Key;
            this.cmdAdd = new RelayCommand<object>(actAdd);
        }

        /// <summary>
        /// Verifie la valeur des champs saisie
        /// </summary>
        /// <param name="o"></param>
        public void actAdd(object o)
        { 
            Student student = new Student();

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
                student.kinship = this.kinship;
                student.birthday = this.birthday;
                student.gender = this.gender;

                customer.students.Add(student);

                db.SaveChanges();
                mediator.NotifyViewModel(Helper.Event.ADD_STUDENT, student);

                this.CloseActionAdd();
            }
        }
    }
}
