﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrototypeEDUCOM.Model;
namespace PrototypeEDUCOM.ViewModel.Customer
{
    class ShowCustomerViewModel : BaseViewModel {
        public string firstname { get; set; }
        public string lastname { get; set; }

        public ShowCustomerViewModel(contact contact)
        {
            this.firstname = contact.firstname;
            this.lastname = contact.lastname;
        }
    }
}