using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Requests.Propositions
{
    class AddPropositionViewModel : BaseViewModel
    {
        public Request request { get; set; }
        public Contact customer { get; set; }
        public Student student { get; set; }
        public List<Program> programs { get; set; }
        public Program program { get; set; }
        public ICommand cmdAdd { get; set; }
        public Action CloseActionAdd { get; set; }

        public AddPropositionViewModel(Request request) { 
            this.student = request.student;
            this.request = request;
            this.customer = request.contact;
            this.programs = db.programs.ToList();

            foreach (Proposition p in this.request.propositions)
                this.programs.Remove(p.program);

            this.cmdAdd = new RelayCommand<Object>(actAdd);
        }

        public void actAdd(Object o)
        {
            Proposition proposition = new Proposition();
            proposition.program = this.program;

            request.propositions.Add(proposition);

            db.SaveChanges();
            mediator.NotifyViewModel(Helper.Event.ADD_PROPOSITION,proposition);

            this.CloseActionAdd();
        }
    }
}
