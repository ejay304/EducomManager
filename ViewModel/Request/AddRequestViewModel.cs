using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Request
{
    class AddRequestViewModel : BaseViewModel
    {
        public contact customer { get; set; }
        public student student { get; set; }

        public ICommand cmdAdd { get; set; }
        public Action CloseActionFormAdd { get; set; }

        public AddRequestViewModel(contact customer) {
            this.cmdAdd = new RelayCommand<Object>(actAdd);
            this.CloseActionFormAdd();  
        }

        public void actAdd(Object o) { 

            _event _event = new _event();
            _event.event_types = db.event_types.First();

            request request = new request();

            
        }
    }
}
