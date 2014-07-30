using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Request.Proposition
{
    class AddInscriptionViewModel : BaseViewModel
    {
        public proposition proposition { get; set; }

        public campu campus { get; set; }

        public ICommand cmdAdd { get; set; }
        public Action CloseActionAdd { get; set; }

        public AddInscriptionViewModel(proposition proposition) 
        {
            this.proposition = proposition;
            this.campus = proposition.program.campus.First();
            this.cmdAdd = new RelayCommand<campu>(actAdd);
        }

        public void actAdd(campu c)
        {
            this.proposition.inscription = true;
            this.proposition.campu = campus;
            db.SaveChanges();

            mediator.NotifyViewModel(Helper.Event.ADD_INSCRIPTION, proposition);
            this.CloseActionAdd();
        }
    }
}
