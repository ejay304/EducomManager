using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisations
{
    class ShowOrganisationViewModel : BaseViewModel
    {

        public Organisation organisation { get; set; }
        public ObservableCollection<Program> programs { get; set; }

        public ICommand cmdEdit { get; set; }
        public ICommand cmdDelete { get; set; }
        public ICommand cmdAddProgram { get; set; }
        public ICommand cmdShowProgram { get; set; } 
        public ShowOrganisationViewModel(Organisation organisation)
        {
            this.organisation = organisation;
            this.cmdEdit = new RelayCommand<Organisation>(actEdit);
            this.cmdDelete = new RelayCommand<Organisation>(actDelete);
            this.cmdAddProgram = new RelayCommand<Organisation>(actAddProgram);
            this.cmdShowProgram = new RelayCommand<Program>(actShowProgram);
     
            db.SaveChanges();
            this.programs = new ObservableCollection<Program>(organisation.programs.ToList());


            mediator.Register(Helper.Event.ADD_PROGRAM, this);
            mediator.Register(Helper.Event.DELETE_PROGRAM, this);
        }

        private void actEdit(Organisation organisation)
        {
            mediator.openEditOrganisationView(this.organisation);
        }

        private void actDelete(Object o)
        {
            mediator.openDeleteOrganisationView(this.organisation);
        }
        private void actAddProgram(Object o)
        {
            mediator.openAddProgramView(this.organisation);
        }
        private void actShowProgram(Program program)
        {
            mediator.openShowProgramView(program);
        }
        public override void Update(string eventName, object item)
        {
            switch (eventName)
            {
                case Helper.Event.ADD_PROGRAM:
                    this.programs.Add((Program)item);
                    NotifyPropertyChanged("programs");
                    break;

                case Helper.Event.DELETE_PROGRAM:
                    this.programs.Remove((Program)item);
                    NotifyPropertyChanged("programs");
                    break;
            }
        }

    }
}
