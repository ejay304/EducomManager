using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisation
{
    class DeleteCampusViewModel : BaseViewModel 
    {

        public campu campus { get; set; }
        public ICommand cmdArchive { get; set; }
        public ICommand cmdDelete { get; set; }
        public int nbrProposition { get; set; }
        public Action CloseActionDelete { get; set; }

        public DeleteCampusViewModel(campu campus){
            this.campus = campus;
            //this.cmdArchive = new RelayCommand<Object>()
            this.cmdDelete = new RelayCommand<Object>(actDelete);
            this.nbrProposition = campus.propositions.Count();
        }

        public void actDelete(Object o) {
            for (int i = 0; i < nbrProposition; i++)
            {
                db.propositions.Remove(campus.propositions.First());
            }
      

            mediator.NotifyViewModel(Helper.Event.DELETE_CAMPUS, campus);
            db.campus.Remove(campus);
            db.SaveChanges();

            this.CloseActionDelete();
        
        }
    
    }
}
