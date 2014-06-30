using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Customer
{
    class ListCustomerViewModel : BaseViewModel
    {
        public ObservableCollection<contact> customers { get; set; }

        public ICommand cmdViewDetail { get; set; }

        public ICommand cmdDelete { get; set; }

        public ICommand cmdFormAddRequest { get; set; }

        public ICommand cmdFormEditRequest { get; set; }

        public ICommand cmdEdit { get; set; }

        public Action CloseActionFormEdit { get; set; }

        public ListCustomerViewModel() : base()
        {
            this.customers = new ObservableCollection<contact>(db.contacts.ToList());
            this.cmdViewDetail = new RelayCommand<request>(actViewDetail);
            this.cmdDelete = new RelayCommand<request>(actDelete);
            this.cmdFormAddRequest = new RelayCommand<object>(actFormAddRequest);
            this.cmdFormEditRequest = new RelayCommand<request>(actFormEditRequest);
           
            this.cmdEdit = new RelayCommand<request>(actEdit);
        }

        private void actFormAddRequest(object obj)
        {

        }

        private void actFormEditRequest(request request)
        {

        }

        public void actViewDetail(request request)
        {

        }

        public void actDelete(request request)
        {

        }

        public void actEdit(object obj)
        {

        }
    }
}
