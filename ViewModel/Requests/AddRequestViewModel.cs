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
    class AddRequestViewModel : BaseViewModel
    {
        public Contact customer { get; set; }
        public Student student { get; set; }
        public string journey { get; set; }
        public Dictionary<string, string> journeys { get { return Dictionaries.journeys; } set { } }
        public ICommand cmdAdd { get; set; }
        public Action CloseActionAdd { get; set; }

        public AddRequestViewModel(Contact customer) {
            this.cmdAdd = new RelayCommand<Object>(actAdd);
            this.customer = customer;
            this.student = customer.students.First();
            this.journey = this.journeys.First().Key;
        }

        public void actAdd(Object o) { 

            Model.Event newEvent = new Model.Event();
            newEvent.event_types = db.event_types.OrderBy(event_type => event_type.order).First();

            Request request = new Request();

            request.events.Add(newEvent);
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
