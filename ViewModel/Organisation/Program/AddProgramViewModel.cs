using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisation
{
    class AddProgramViewModel : BaseViewModel
    {
        public organisation organisation { get; set; }
        public List<program_types> types { get; set; }
        public int typeIndex { get; set; }
        public DateTime? begin_date { get; set; }
        public DateTime? end_date { get; set; }
        public Action CloseActionAdd { get; set; }
        public ICommand cmdAdd { get; set; }
        public AddProgramViewModel(organisation organisation) {
            this.organisation = organisation;

            this.types = db.program_types.ToList();
            this.begin_date = DateTime.Now;
            this.end_date = DateTime.Now;
         
            this.cmdAdd = new RelayCommand<Object>(actAdd);
        }

        private void actAdd(object obj)
        {
            program program = new program();

            program.begin_date = begin_date;
            program.end_date = end_date;
            program.program_types = types.ElementAt(typeIndex);

            organisation.programs.Add(program);
            db.SaveChanges();

            mediator.NotifyViewModel(Helper.Event.ADD_PROGRAM, program);

            this.CloseActionAdd();
            
        }
    }
}
