using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Customer
{
    class DeleteStudentViewModel : BaseViewModel
    {
        public student student { get; set; }
        public ICommand cmdArchive { get; set; }
        public ICommand cmdDelete { get; set; }
        public int nbrRequest { get; set; }
        public Action CloseActionDelete { get; set; }

        public DeleteStudentViewModel(student student)
        {
            this.cmdDelete = new RelayCommand<Object>(actDeleteStudent);
            this.student = student;
            this.nbrRequest = student.requests.Count();
        }

        private void actDeleteStudent(Object o)
        {
            for (int i = 0; i < nbrRequest; i++)
            {
                db.requests.Remove(student.requests.First());
            }

            mediator.NotifyViewModel(Helper.Event.DELETE_STUDENT, student);

            db.students.Remove(student);
            db.SaveChanges();

            this.CloseActionDelete();
        }
    }
}
