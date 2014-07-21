using PrototypeEDUCOM.Helper.Enum;
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
        public Journey journey { get; set; }
        public List<Journey> journeys { get; set; }
        public ICommand cmdAdd { get; set; }
        public Action CloseActionAdd { get; set; }

        public AddRequestViewModel(contact customer) {
            this.cmdAdd = new RelayCommand<Object>(actAdd);
            this.customer = customer;
            this.students = customer.students.ToList();
            this.student = this.students.First();
            this.journeys = Helper.Enum.Journey.list;
            this.journey = this.journeys.First();
        }

        public void actAdd(Object o) { 

            _event _newEvent = new _event();
            _newEvent.event_types = db.event_types.OrderBy(event_type => event_type.order).First();

            request request = new request();

            request.events.Add(_newEvent);
            request.contact = this.customer;
            request.user = mediator.user;
            request.journey_type = journey.getValue();
            request.student = student;
            request.creation_date = DateTime.Now;

            db.requests.Add(request);

            db.SaveChanges();

            mediator.NotifyViewModel(Helper.Event.ADD_REQUEST, request);
            this.CloseActionAdd();  
        }
    }
}
