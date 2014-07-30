using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisations.Programs
{
    class EditProgramViewModel :BaseViewModel
    {
        public Program program { get; set; }
        public List<ProgramType> programTypes { get; set; }
        public ProgramType programType { get; set; }
        public DateTime? begin_date { get; set; }
        public DateTime? end_date { get; set; }
        public Action CloseActionEdit { get; set; }

        public ICommand cmdEdit { get; set; }
        public EditProgramViewModel(Program program)
        {
            this.program = program;
            this.programTypes = db.program_types.ToList();
            this.programType = program.program_types;
            this.begin_date = program.begin_date;
            this.end_date = program.end_date;

            this.cmdEdit = new RelayCommand<Object>(actEdit);
        }

        public void actEdit(Object o) {

            program.begin_date = this.begin_date;
            program.end_date = this.end_date;
            program.program_types = programType;

            db.SaveChanges();

            this.CloseActionEdit();
        
        }
    }
}
