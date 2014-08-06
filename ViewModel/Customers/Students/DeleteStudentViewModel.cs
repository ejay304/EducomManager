using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Customers.Students
{
    /// <filename>DeleteStudentViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère la fenêtre de suppression d'étudiants</summary>
    class DeleteStudentViewModel : BaseViewModel
    {
        public Student student { get; set; }
        public ICommand cmdArchive { get; set; }
        public ICommand cmdDelete { get; set; }
        public int nbrRequest { get; set; }
        public Action CloseActionDelete { get; set; }

        /// <summary>
        /// Initialise les valeur à binder et lie la command de suppression à l'action
        /// </summary>
        /// <param name="student">L'étudiant à supprimer</param>
        public DeleteStudentViewModel(Student student)
        {
            this.cmdDelete = new RelayCommand<Object>(actDeleteStudent);
            this.student = student;
            this.nbrRequest = student.requests.Count();
        }

       /// <summary>
        /// Supprimer l'étudiant ainsi que toutes ses dépendances
       /// </summary>
       /// <param name="o"></param>
        private void actDeleteStudent(Object o)
        {
            mediator.NotifyViewModel(Helper.Event.DELETE_STUDENT, student);

            for (int i = 0; i < nbrRequest; i++)
            {
                int nbrEvent = student.requests.First().events.Count;

                //Suppression des évènements
                for (int j = 0; j < nbrEvent; j++)
                    db.events.Remove(student.requests.First().events.First());

                db.requests.Remove(student.requests.First());
            }

            db.students.Remove(student);
            db.SaveChanges();

            this.CloseActionDelete();
        }
    }
}
