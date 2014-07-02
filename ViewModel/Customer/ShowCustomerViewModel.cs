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

        public ViewModel.Customer.CustomerViewModel parentVM;
        public contact contact { get; set; }
        public ObservableCollection<student> students { get; set; }

        public ICommand cmdEditCustomer { get; set; }
        public ICommand cmdDeleteCustomer { get; set; }
        public ICommand cmdAddStudent { get; set; }

        public ShowCustomerViewModel(contact contact, ViewModel.Customer.CustomerViewModel parentVM)
        {
            this.parentVM = parentVM;
            this.contact = contact;
            this.students = new ObservableCollection<student>(contact.students.ToList());
            this.cmdEditCustomer = new RelayCommand<Object>(actEditCustomer);
            this.cmdDeleteCustomer = new RelayCommand<Object>(actDeleteCustomer);
            this.cmdAddStudent = new RelayCommand<Object>(actAddStudent);


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
            DeleteCustomerViewModel deleteCustomerViewModel = new DeleteCustomerViewModel(contact);
            DeleteCustomerView deleteCustomerView = new DeleteCustomerView(contact);

            deleteCustomerView.DataContext = deleteCustomerViewModel;
            deleteCustomerViewModel.CloseActionDelete = new Action(() => deleteCallback(deleteCustomerView));
            
            deleteCustomerView.ShowDialog();

        }
        private void deleteCallback(DeleteCustomerView view){
            view.Close();
            if (tabs.First() != parentVM.selectedTab) {
                parentVM.customerTabs.Remove(parentVM.selectedTab);
                parentVM.NotifyPropertyChanged("customerTabs");
                parentVM.selectedTab = tabs.First();
                parentVM.NotifyPropertyChanged("selectedTab");
            }
        }

        public void actAddStudent(object o)
        {
            AddStudentModelView addStudentModelView = new AddStudentModelView();
            AddStudentView addStudentView = new AddStudentView();

            addStudentView.DataContext = addStudentModelView;
            //addStu.CloseActionDelete = new Action(() => deleteCallback(deleteCustomerView));

            addStudentView.Show();
        }
    }
}
