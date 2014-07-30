
﻿using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Customer.Student
{
    class AddStudentViewModel : BaseViewModel
    {
        public contact customer { get; set; }
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
        public Validation validBirthday { get; set; }
        public ICommand cmdAdd { get; set; }
        public Action CloseActionAdd { get; set; }
    
        public AddStudentViewModel(contact customer) {

            this.customer = customer;
            this.birthday = DateTime.Now;
            this.kinship = kinships.First().Key;
            this.gender = genders.First().Key;
            this.cmdAdd = new RelayCommand<object>(actAdd);
        }
        public void actAdd(object o)
        { 
            student student = new student();

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

            student.kinship = this.kinship;
            student.birthday = birthday;
            student.gender = this.gender;

            if (!error)
            {
                customer.students.Add(student);

                db.SaveChanges();
                mediator.NotifyViewModel(Helper.Event.ADD_STUDENT, student);

                this.CloseActionAdd();
            }
        }
    }
}
