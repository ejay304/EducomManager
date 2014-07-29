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
    class AddRequestViewModel : BaseViewModel
    {
        public contact customer { get; set; }
        public student student { get; set; }
        public List<student> students { get; set; }
        public string journey { get; set; }
        public Dictionary<string, string> journeys { get { return Dictionaries.journeys; } set { } }
        public ICommand cmdAdd { get; set; }
        public Action CloseActionAdd { get; set; }

        public AddRequestViewModel(contact customer) {
            this.cmdAdd = new RelayCommand<Object>(actAdd);
            this.customer = customer;
            this.students = customer.students.ToList();
            if(students.Count != 0)
                this.student = this.students.First();
            this.journey = this.journeys.First().Value;
        }

        public void actAdd(Object o) { 

            _event _newEvent = new _event();
            _newEvent.event_types = db.event_types.OrderBy(event_type => event_type.order).First();

            request request = new request();

            request.events.Add(_newEvent);
            request.contact = this.customer;
            request.user = mediator.user;
            request.journey_type = this.journey;
            request.student = student;
            request.creation_date = DateTime.Now;

            db.requests.Add(request);

            db.SaveChanges();

            mediator.NotifyViewModel(Helper.Event.ADD_REQUEST, request);
            this.CloseActionAdd();  
        }
    }
}
