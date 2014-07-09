using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisation
{
    class ShowOrganisationViewModel : BaseViewModel
    {

        public organisation organisation { get; set; }
        public ICommand cmdEdit { get; set; }
        public ICommand cmdDelete { get; set; }

        public ShowOrganisationViewModel(organisation organisation)
        {
            this.organisation = organisation;
            this.cmdEdit = new RelayCommand<organisation>(actEdit);
            this.cmdDelete = new RelayCommand<organisation>(actDelete);

            mediator.Register(Helper.Event.ADD_ORGANISATION, this);
            mediator.Register(Helper.Event.DELETE_ORGANISATION, this);
        }

        private void actEdit(organisation organisation)
        {
            throw new NotImplementedException();
        }

        private void actDelete(organisation organisation)
        {
            mediator.openDeleteOrganisationView(organisation);
        }
    }
}
