using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisation
{
    class AddCampusViewModel : BaseViewModel 
    {
        public program program { get; set; }
        public string name { get; set; }
        public ICommand cmdAdd { get; set; }
        public Action CloseActionAdd { get; set; }
      
        public AddCampusViewModel(program  program) {
            this.program = program;
            this.cmdAdd = new RelayCommand<Object>(actAdd);
        }
        public void actAdd(Object o) {
            campu campus = new campu();
            campus.name = this.name;
            program.campus.Add(campus);

            db.SaveChanges();
            mediator.NotifyViewModel(Helper.Event.ADD_CAMPUS, campus);

            this.CloseActionAdd();
        }

    }
}
