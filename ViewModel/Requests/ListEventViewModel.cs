using PrototypeEducom.Helper;
using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeEDUCOM.ViewModel.Requests
{
    class ListEventViewModel : BaseViewModel
    {
        public SortableObservableCollection<Event> events { get; set; }


        public ListEventViewModel(Request request)
        {
            this.events = new SortableObservableCollection<Event>(request.events.ToList());
        }
    }
}
