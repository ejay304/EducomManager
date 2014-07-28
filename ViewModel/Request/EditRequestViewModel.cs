using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Request
{
    class EditRequestViewModel : BaseViewModel
    {
        public request currentRequest { get; set; }
        public student student { get; set; }
        public List<student> students { get; set; }
        public string journey { get; set; }
        public Dictionary<string, string> journeys { get { return Dictionaries.journeys; } set { } }

        public ICommand cmdEdit { get; set; }
        public Action CloseActionEdit { get; set; }


        public EditRequestViewModel(request request)
        {
            currentRequest = request;
            this.journey = request.journey_type;
            this.student = request.student;
            this.students = request.contact.students.ToList();

            this.cmdEdit = new RelayCommand<object>(actEdit);
        }

        public void actEdit(object obj)
        {
            currentRequest.journey_type = journey;
            currentRequest.student = student;

            db.SaveChanges();

            this.CloseActionEdit();  
        }
    }
}
