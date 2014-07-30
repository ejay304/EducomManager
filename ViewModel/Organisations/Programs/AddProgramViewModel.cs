using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisations.Programs
{
    class AddProgramViewModel : BaseViewModel
    {
        public Organisation organisation { get; set; }
        public List<ProgramType> programTypes { get; set; }
        public ProgramType programType { get; set; }
        public DateTime? begin_date { get; set; }
        public DateTime? end_date { get; set; }
        public Action CloseActionAdd { get; set; }
        public ICommand cmdAdd { get; set; }
        public AddProgramViewModel(Organisation organisation) {
            this.organisation = organisation;
            this.programTypes = db.program_types.ToList();
            this.programType = this.programTypes.First();
            this.begin_date = DateTime.Now;
            this.end_date = DateTime.Now;
         
            this.cmdAdd = new RelayCommand<Object>(actAdd);
        }

        private void actAdd(object obj)
        {
            Program program = new Program();

            program.begin_date = begin_date;
            program.end_date = end_date;
            program.program_types = programType;

            organisation.programs.Add(program);
            db.SaveChanges();

            mediator.NotifyViewModel(Helper.Event.ADD_PROGRAM, program);

            this.CloseActionAdd();
            
        }
    }
}
