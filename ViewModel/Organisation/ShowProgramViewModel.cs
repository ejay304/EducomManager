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

        public ICommand cmdEdit { get; set; }
        public ICommand cmdDelete { get; set; }
        public ICommand cmdAddCampus { get; set; }
        public ICommand cmdDeleteCampus { get; set; }

        public program program { get; set; }
        public ShowProgramViewModel(program program) {
            this.program = program;
            this.campuses = new ObservableCollection<campu>(program.campus.ToList());
            this.cmdEdit = new RelayCommand<Object>(actEdit);
            this.cmdDelete = new RelayCommand<Object>(actDelete);
            this.cmdAddCampus = new RelayCommand<Object>(actAddCampus);
            this.cmdDeleteCampus = new RelayCommand<campu>(actDeleteCampus);

            mediator.Register(Helper.Event.ADD_CAMPUS, this);
        }

        public void actEdit(Object o){
            mediator.openEditProgramView(this.program);
        }

        public void actDelete(Object o){
            mediator.openDeleteProgramView(this.program);
        }
        public void actAddCampus(Object o) {
            mediator.openAddCampusView(program);
        }
        public void actDeleteCampus(campu campus) {
            mediator.openDeleteCampusView(campus);
        }
        public override void Update(string eventName, object item)
        {
            switch (eventName)
            {
                case Helper.Event.ADD_CAMPUS:
                    this.campuses.Add((campu)item);
                    NotifyPropertyChanged("campuses");
                    break;

                case Helper.Event.DELETE_CAMPUS:
                    this.campuses.Remove((campu)item);
                    NotifyPropertyChanged("campuses");
                    break;
            }
        }
    }
}
