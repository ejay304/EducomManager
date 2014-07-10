using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisation
{
    class DeleteProgramViewModel : BaseViewModel
    {
        public program program { get; set; }
        public ICommand cmdArchive { get; set; }
        public ICommand cmdDelete { get; set; }
        public int nbrContact { get; set; }
        public int nbrCampus { get; set; }

        public Action CloseActionDelete { get; set; }
        public DeleteProgramViewModel(program program) {
            this.program = program;
            this.nbrContact = program.contacts.Count();
            this.nbrCampus = program.campus.Count();
            this.cmdDelete = new RelayCommand<Object>(actDelete);
        }
        public void actDelete(Object o) {

            for (int i = 0; i < nbrContact; i++)
            {
                db.contacts.Remove(program.contacts.First());
            }
            for (int i = 0; i < nbrCampus; i++)
            {
                db.campus.Remove(program.campus.First());
            }

            mediator.NotifyViewModel(Helper.Event.DELETE_PROGRAM, program);
            db.programs.Remove(program);
            db.SaveChanges();

            this.CloseActionDelete();
        }
    }
}
