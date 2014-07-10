using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeEDUCOM.ViewModel.Organisation
{
    class EditOrganisationViewModel :BaseViewModel
    {
        public Action CloseActionFormEdit { get; set; }
        public EditOrganisationViewModel(organisation organisation) {

            this.CloseActionFormEdit();
        
        }
    }
}
