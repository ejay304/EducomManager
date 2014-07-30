using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisations.Programs
{
    class ShowProgramViewModel : BaseViewModel
    {
        public Program program { get; set; }
        public ObservableCollection<Campus> campuses { get; set; }

        public bool editDescription { get; set; }

        public ICommand cmdEdit { get; set; }
        public ICommand cmdDelete { get; set; }

        public ICommand cmdAddCampus { get; set; }

        public ICommand cmdEditCampus { get; set; }
        public ICommand cmdDeleteCampus { get; set; }

        public ICommand cmdEditDescription { get; set; }
    

        public string description { get; set; }

        public ShowProgramViewModel(Program program) {

            this.program = program;
            this.description = program.description;

            this.campuses = new ObservableCollection<Campus>(program.campus.ToList());
            this.cmdEdit = new RelayCommand<Object>(actEdit);
            this.cmdDelete = new RelayCommand<Object>(actDelete);

            this.cmdEditDescription = new RelayCommand<Object>(actEditDescription);
            this.editDescription = false;

            this.cmdAddCampus = new RelayCommand<Object>(actAddCampus);
            this.cmdEditCampus = new RelayCommand<Campus>(actEditCampus);
            this.cmdDeleteCampus = new RelayCommand<Campus>(actDeleteCampus);

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

        public void actEditCampus(Campus campus)
        {
            mediator.openEditCampusView(campus);
        }

        public void actDeleteCampus(Campus campus) {
            mediator.openDeleteCampusView(campus);
        }
        public override void Update(string eventName, object item)
        {
            switch (eventName)
            {
                case Helper.Event.ADD_CAMPUS:
                    this.campuses.Add((Campus)item);
                    NotifyPropertyChanged("campuses");
                    break;

                case Helper.Event.DELETE_CAMPUS:
                    this.campuses.Remove((Campus)item);
                    NotifyPropertyChanged("campuses");
                    break;
            }
        }
    }
}
