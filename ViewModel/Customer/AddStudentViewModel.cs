
﻿using PrototypeEDUCOM.Helper;

﻿using PrototypeEDUCOM.Helper.Enum;
using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Customer
{
    class AddStudentViewModel : BaseViewModel
    {
        public contact customer { get; set; }
        public String firstname { get; set; }
        public DateTime birthday { get; set; }
        public String lastname { get; set; }
        public List<Kinship> kinships { get; set; }
        public int kinshipIndex { get; set; }
        public String gender { get; set; }
        public Validation validFirstname { get; set; }
        public Validation validLastname { get; set; }
        public Validation validGender { get; set; }
        public Validation validBirthday { get; set; }
        public ICommand cmdAdd { get; set; }
        public Action CloseActionAdd { get; set; }
        public ShowCustomerViewModel parentVM { get; set; }

        public AddStudentViewModel(contact customer, ShowCustomerViewModel parentVM) {

            this.customer = customer;
            this.kinships = Kinship.list;
            this.cmdAdd = new RelayCommand<object>(actAdd);
            this.parentVM = parentVM;
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
                customer.lastname = this.lastname;
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

            student.kinship = kinships.ElementAt(kinshipIndex).getValue();
            student.gender = "man";

            if (!error)
            {
                customer.students.Add(student);

                db.SaveChanges();

                parentVM.students.Add(student);
                parentVM.NotifyPropertyChanged("students");

                this.CloseActionAdd();
            }
        }
    }
}
