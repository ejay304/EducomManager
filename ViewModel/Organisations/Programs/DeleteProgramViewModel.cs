using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisations.Programs
{
    /// <filename>DeleteProgramViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère la fenêtre de suppression de programmes</summary>
    class DeleteProgramViewModel : BaseViewModel
    {
        public Program program { get; set; }
        public ICommand cmdArchive { get; set; }
        public ICommand cmdDelete { get; set; }
        public int nbrProposition { get; set; }
        public int nbrCampus { get; set; }

        public Action CloseActionDelete { get; set; }

        /// <summary>
        ///  Initialise les valeurs à binder et lie la commande de suppression à l'action
        /// </summary>
        /// <param name="program"></param>
        public DeleteProgramViewModel(Program program) {
            this.program = program;
            this.nbrProposition = program.propositions.Count();
            this.nbrCampus = program.campus.Count();
            this.cmdDelete = new RelayCommand<Object>(actDelete);
        }

        /// <summary>
        /// Suppression du programme et de toutes ces dépendences
        /// </summary>
        /// <param name="o"></param>
        public void actDelete(Object o) {

            for (int i = 0; i < nbrProposition; i++)
            {
                db.propositions.Remove(program.propositions.First());
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
