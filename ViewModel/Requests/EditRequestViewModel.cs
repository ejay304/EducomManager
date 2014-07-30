using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Requests
{
    class EditRequestViewModel : BaseViewModel
    {
        public Request request { get; set; }
        public Student student { get; set; }
        public string journey { get; set; }
        public Dictionary<string, string> journeys { get { return Dictionaries.journeys; } set { } }

        public ICommand cmdEdit { get; set; }
        public Action CloseActionEdit { get; set; }


        public EditRequestViewModel(Request request)
        {
            this.request = request;
            this.journey = request.journey_type;
            this.student = request.student;
        
            this.cmdEdit = new RelayCommand<object>(actEdit);
        }

        public void actEdit(object obj)
        {
            this.request.journey_type = journey;
            this.request.student = student;

            db.SaveChanges();

            this.CloseActionEdit();  
        }
    }
}
