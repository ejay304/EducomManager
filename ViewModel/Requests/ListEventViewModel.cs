using PrototypeEDUCOM.Helper;
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
        public SortableObservableCollection<Model.Event> events { get; set; }

        /// <summary>
        /// Initialise la liste d'événement à afficher
        /// </summary>
        /// <param name="request"></param>
        public ListEventViewModel(Request request)
        {
            this.events = new SortableObservableCollection<Model.Event>(request.events.ToList());
        }
    }
}
