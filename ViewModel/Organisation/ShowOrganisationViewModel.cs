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
    class ShowOrganisationViewModel : BaseViewModel
    {

        public organisation organisation { get; set; }
        public ObservableCollection<program> programs { get; set; }

        public ICommand cmdEdit { get; set; }
        public ICommand cmdDelete { get; set; }
        public ICommand cmdAddProgram { get; set; }
        public ICommand cmdShowProgram { get; set; } 
        public ShowOrganisationViewModel(organisation organisation)
        {
            this.organisation = organisation;
            this.cmdEdit = new RelayCommand<organisation>(actEdit);
            this.cmdDelete = new RelayCommand<organisation>(actDelete);
            this.cmdAddProgram = new RelayCommand<organisation>(actAddProgram);
            this.cmdShowProgram = new RelayCommand<program>(actShowProgram);
     
            db.SaveChanges();
            this.programs = new ObservableCollection<program>(organisation.programs.ToList());


            mediator.Register(Helper.Event.ADD_PROGRAM, this);
            mediator.Register(Helper.Event.DELETE_PROGRAM, this);
        }

        private void actEdit(organisation organisation)
        {
            throw new NotImplementedException();
        }

        private void actDelete(Object o)
        {
            mediator.openDeleteOrganisationView(this.organisation);
        }
        private void actAddProgram(Object o)
        {
            mediator.openAddProgramView(this.organisation);
        }
        private void actShowProgram(program program)
        {
            mediator.openShowProgramView(program);
        }
        public override void Update(string eventName, object item)
        {
            switch (eventName)
            {
                case Helper.Event.ADD_PROGRAM:
                    this.programs.Add((program)item);
                    NotifyPropertyChanged("programs");
                    break;

                case Helper.Event.DELETE_PROGRAM:
                    this.programs.Remove((program)item);
                    NotifyPropertyChanged("programs");
                    break;
            }
        }

    }
}
