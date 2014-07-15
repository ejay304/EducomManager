using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisation
{
    class ShowProgramViewModel : BaseViewModel
    {
        public ObservableCollection<campu> campuses { get; set; }

        public bool editDescription { get; set; }

        public ICommand cmdEdit { get; set; }
        public ICommand cmdDelete { get; set; }

        public ICommand cmdEditDescription { get; set; }
    
        public program program { get; set; }

        public ShowProgramViewModel(program program) {
            this.program = program;
            this.campuses = new ObservableCollection<campu>(program.campus.ToList());
            this.cmdEdit = new RelayCommand<Object>(actEdit);
            this.cmdDelete = new RelayCommand<Object>(actDelete);
            this.cmdEditDescription = new RelayCommand<Object>(actEditDescription);

            this.editDescription = false;
        }

        public void actEdit(Object o){
            mediator.openEditProgramView(this.program);
        }

        public void actDelete(Object o){
            mediator.openDeleteProgramView(this.program);
        }

        public void actEditDescription(Object o)
        {
            if (editDescription)
            {
                db.SaveChanges();
            }

            editDescription = !editDescription;
            NotifyPropertyChanged("editDescription");
        }
    }
}
