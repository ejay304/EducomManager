using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Request.Proposition
{
    class AddPropositionViewModel : BaseViewModel
    {
        public request request { get; set; }
        public contact customer { get; set; }
        public student student { get; set; }
        public List<program> programs { get; set; }
        public program program { get; set; }
        public ICommand cmdAdd { get; set; }
        public Action CloseActionAdd { get; set; }

        public AddPropositionViewModel(request request) { 
            this.student = request.student;
            this.request = request;
            this.customer = request.contact;
            this.programs = db.programs.ToList();

            foreach (proposition p in this.request.propositions)
                this.programs.Remove(p.program);

            this.cmdAdd = new RelayCommand<Object>(actAdd);
        }

        public void actAdd(Object o)
        {
            proposition proposition = new proposition();
            proposition.program = this.program;

            request.propositions.Add(proposition);

            db.SaveChanges();
            mediator.NotifyViewModel(Helper.Event.ADD_PROPOSITION,proposition);

            this.CloseActionAdd();
        }
    }
}
