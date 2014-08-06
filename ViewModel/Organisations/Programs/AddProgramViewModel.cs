using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisations.Programs
{
    /// <filename>AddProgramViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère la fenêtre d'ajout de programme</summary>
    class AddProgramViewModel : BaseViewModel
    {
        public Organisation organisation { get; set; }
        public List<ProgramType> programTypes { get; set; }
        public ProgramType programType { get; set; }
        public DateTime beginDate { get; set; }
        public DateTime endDate { get; set; }
        public Action CloseActionAdd { get; set; }
        public ICommand cmdAdd { get; set; }


        /// <summary>
        /// Initialise les valeurs bindé et lie la command à l'action
        /// </summary>
        /// <param name="organisation">l'organisation liée au programme</param>
        public AddProgramViewModel(Organisation organisation) {
            this.organisation = organisation;
            this.programTypes = db.program_types.ToList();
            this.programType = this.programTypes.First();
            this.beginDate = DateTime.Now;
            this.endDate = DateTime.Now;
         
            this.cmdAdd = new RelayCommand<Object>(actAdd);
        }

        /// <summary>
        /// Ajoute le programme avec les données saisie dans le formulaire
        /// </summary>
        /// <param name="obj"></param>
        private void actAdd(object obj)
        {
            Program program = new Program();

            program.begin_date = beginDate;
            program.end_date = endDate;
            program.program_types = programType;

            organisation.programs.Add(program);
            db.SaveChanges();

            mediator.NotifyViewModel(Helper.Event.ADD_PROGRAM, program);

            this.CloseActionAdd();
            
        }
    }
}
