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

        private ShowCustomerViewModel parentVM;

        public DeleteStudentViewModel(student student,ShowCustomerViewModel parentVM)
        {
            this.cmdDelete = new RelayCommand<Object>(actDeleteStudent);
            this.student = student;
            this.nbrRequest = student.requests.Count();
            this.parentVM = parentVM;
        }

        private void actDeleteStudent(Object o)
        {
            for (int i = 0; i < nbrRequest; i++)
            {
                db.requests.Remove(student.requests.First());
            }

            db.students.Remove(student);
            db.SaveChanges();

            parentVM.students.Remove(student);
            parentVM.NotifyPropertyChanged("students");
            this.CloseActionDelete();
        }
    }
}
