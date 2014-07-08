﻿using System;
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

        public contact contact { get; set; }
        public ObservableCollection<student> students { get; set; }

        public ICommand cmdEditCustomer { get; set; }
        public ICommand cmdDeleteCustomer { get; set; }
        public ICommand cmdAddStudent { get; set; }
        public ICommand cmdEditStudent { get; set; }
        public ICommand cmdDeleteStudent { get; set; }

        public ShowCustomerViewModel(contact contact)
        {
            //this.parentVM = parentVM;
            this.contact = contact;
            this.students = new ObservableCollection<student>(contact.students.ToList());
            this.cmdEditCustomer = new RelayCommand<Object>(actEditCustomer);
            this.cmdDeleteCustomer = new RelayCommand<Object>(actDeleteCustomer);
            this.cmdAddStudent = new RelayCommand<Object>(actAddStudent);
            this.cmdEditStudent = new RelayCommand<student>(actEditStudent);
            this.cmdDeleteStudent = new RelayCommand<student>(actDeleteStudent);
        }

        public void actEditCustomer(object o)
        {
            EditCustomerViewModel editCustomerViewModel = new EditCustomerViewModel(contact);
            EditCustomerView editCustomerView = new EditCustomerView();

            editCustomerView.DataContext = editCustomerViewModel;
            editCustomerViewModel.CloseActionFormAdd = new Action(() => editCustomerView.Close());

            editCustomerView.Show();
        }
        public void actDeleteCustomer(object o)
        {
            mediator.openDeleteCustomerView(this.contact);
        }

        public void actAddStudent(object o)
        {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(contact,this);
            AddStudentView addStudentView = new AddStudentView();

            addStudentView.DataContext = addStudentViewModel;
            addStudentViewModel.CloseActionAdd = new Action(() => addStudentView.Close());

            addStudentView.Show();
        }
        public void actEditStudent(student student)
        {
            EditStudentViewModel editStudentViewModel = new EditStudentViewModel(student, this);
            EditStudentView editStudentView = new EditStudentView();

            editStudentView.DataContext = editStudentViewModel;
            editStudentViewModel.CloseActionEdit = new Action(() => editStudentView.Close());

            editStudentView.Show();
        }
        public void actDeleteStudent(student student)
        {
          //TODO DELETE student

        }
    }
}
