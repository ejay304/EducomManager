using PrototypeEducom.Helper;
using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeEDUCOM.ViewModel.Request
{
    class ListEventViewModel : BaseViewModel
    {
        public SortableObservableCollection<_event> events { get; set; }


        public ListEventViewModel(request request)
        {
            this.events = new SortableObservableCollection<_event>(request.events.ToList());
        }
    }
}
