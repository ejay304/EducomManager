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
        public string civilite { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public DateTime? add_date { get; set; }
        public string street { get; set; }
        public string zip { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string country { get; set; }
        public contact contact { get; set; }
        public ObservableCollection<student> students { get; set; }

        public ICommand cmdEditRequest { get; set; }

        public ShowCustomerViewModel(contact contact)
        {
            this.contact = contact;
            this.civilite = contact.civility;
            this.firstname = contact.firstname;
            this.lastname = contact.lastname;
            this.add_date = contact.add_date;
            this.street = contact.street;
            this.zip = contact.zip;
            this.city = contact.city;
            this.district = contact.district;
            this.country = contact.country;
            this.students = new ObservableCollection<student>(contact.students.ToList());
            this.cmdEditRequest = new RelayCommand<Object>(actEditCommand);
        }

        public void actEditCommand(object o){
            EditCustomerViewModel editCustomerViewModel = new EditCustomerViewModel(contact);
            EditCustomerView editCustomerView = new EditCustomerView();

            editCustomerView.DataContext = editCustomerViewModel;
            editCustomerViewModel.CloseActionFormAdd = new Action(() => editCustomerView.Close());

            editCustomerView.Show();
        
        }
    }
}
