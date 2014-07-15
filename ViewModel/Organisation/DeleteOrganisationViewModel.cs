using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisation
{
    class DeleteOrganisationViewModel : BaseViewModel
    {
        public organisation organisation { get; set; }
        public ICommand cmdArchive { get; set; }
        public ICommand cmdDelete { get; set; }
        public int nbrContact { get; set; }
        public int nbrProgram { get; set; }
  
        public Action CloseActionDelete { get; set; }

        public DeleteOrganisationViewModel(organisation organisation) {

            this.cmdDelete = new RelayCommand<Object>(actDelete);
            this.organisation = organisation;
            this.nbrContact = organisation.contacts.Count();
            this.nbrProgram = organisation.programs.Count();
        }

        private void actDelete(object obj)
        {
            for (int i = 0; i < nbrContact; i++)
            {
                db.contacts.Remove(organisation.contacts.First());
            }
            for (int i = 0; i < nbrProgram; i++)
            {
                db.programs.Remove(organisation.programs.First());
            }

            db.organisations.Remove(organisation);
            mediator.NotifyViewModel(Helper.Event.DELETE_ORGANISATION, organisation);
            db.SaveChanges();

            this.CloseActionDelete();
        }
    }
}
