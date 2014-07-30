using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Requests.Propositions
{
    class AddInscriptionViewModel : BaseViewModel
    {
        public Proposition proposition { get; set; }

        public Campus campus { get; set; }

        public ICommand cmdAdd { get; set; }
        public Action CloseActionAdd { get; set; }

        public AddInscriptionViewModel(Proposition proposition) 
        {
            this.proposition = proposition;
            this.campus = proposition.program.campus.First();
            this.cmdAdd = new RelayCommand<Campus>(actAdd);
        }

        public void actAdd(Campus c)
        {
            this.proposition.inscription = true;
            this.proposition.campu = campus;
            db.SaveChanges();

            mediator.NotifyViewModel(Helper.Event.ADD_INSCRIPTION, proposition);
            this.CloseActionAdd();
        }
    }
}
