using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisation
{
    class EditProgramViewModel :BaseViewModel
    {
        public program program { get; set; }
        public List<program_types> types { get; set; }
        public int typeIndex { get; set; }
        public DateTime? begin_date { get; set; }
        public DateTime? end_date { get; set; }
        public Action CloseActionEdit { get; set; }

        public ICommand cmdEdit { get; set; }
        public EditProgramViewModel(program program)
        {
            this.program = program;
            this.types = db.program_types.ToList();
            //this.typeIndex 
            this.begin_date = program.begin_date;
            this.end_date = program.end_date;

            this.cmdEdit = new RelayCommand<Object>(actEdit);
        }

        public void actEdit(Object o) {

            program.begin_date = this.begin_date;
            program.end_date = this.end_date;
            program.program_types = types.ElementAt(typeIndex);

            db.SaveChanges();

            this.CloseActionEdit();
        
        }
    }
}
