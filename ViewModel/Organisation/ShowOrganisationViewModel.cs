using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeEDUCOM.ViewModel.Organisation
{
    class ShowOrganisationViewModel : BaseViewModel
    {
        public organisation organisation { get; set; }

        public ShowOrganisationViewModel(organisation organisation)
        {
            this.organisation = organisation;
        }
    }
}
