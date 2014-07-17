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

        public ICommand cmdAddCampus { get; set; }

        public ICommand cmdEditCampus { get; set; }
        public ICommand cmdDeleteCampus { get; set; }

        public ICommand cmdEditDescription { get; set; }
    
        public program program { get; set; }

        public string description { get; set; }

        public ShowProgramViewModel(program program) {

            this.program = program;
            this.description = program.description;

            this.campuses = new ObservableCollection<campu>(program.campus.ToList());
            this.cmdEdit = new RelayCommand<Object>(actEdit);
            this.cmdDelete = new RelayCommand<Object>(actDelete);

            this.cmdEditDescription = new RelayCommand<Object>(actEditDescription);
            this.editDescription = false;

            this.cmdAddCampus = new RelayCommand<Object>(actAddCampus);
            this.cmdEditCampus = new RelayCommand<campu>(actEditCampus);
            this.cmdDeleteCampus = new RelayCommand<campu>(actDeleteCampus);

            mediator.Register(Helper.Event.ADD_CAMPUS, this);
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
                program.description = description;
                db.SaveChanges();
            }

            editDescription = !editDescription;
            NotifyPropertyChanged("editDescription");
        }

        public void actAddCampus(Object o) {
            mediator.openAddCampusView(program);
        }

        public void actEditCampus(campu campus)
        {
            mediator.openEditCampusView(campus);
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
