using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Requests.Propositions
{
    /// <filename>AddPropositionViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère la fenêtre d'ajout de recommandaiton d'une demande</summary>
    class AddPropositionViewModel : BaseViewModel
    {
        public Request request { get; set; }
        public Contact customer { get; set; }
        public Student student { get; set; }
        public List<Program> programs { get; set; }
        public Program program { get; set; }
        public ICommand cmdAdd { get; set; }
        public Action CloseActionAdd { get; set; }

        /// <summary>
        /// Initialise les valeurs à binder et lie la commande d'ajout à l'action
        /// </summary>
        /// <param name="request"></param>
        public AddPropositionViewModel(Request request) { 
            this.student = request.student;
            this.request = request;
            this.customer = request.contact;
            this.programs = db.programs.ToList();
            this.program = programs.First();

            foreach (Proposition p in this.request.propositions)
                this.programs.Remove(p.program);

            this.cmdAdd = new RelayCommand<Object>(actAdd);
        }

        /// <summary>
        ///     Ajout de la propositiona avec les valeurs du formulaire
        /// </summary>
        /// <param name="o"></param>
        public void actAdd(Object o)
        {
            Proposition proposition = new Proposition();
            proposition.program = this.program;
            proposition.inscription = false;

            request.propositions.Add(proposition);

            db.SaveChanges();
            mediator.NotifyViewModel(Helper.Event.ADD_PROPOSITION,proposition);

            this.CloseActionAdd();
        }
    }
}
