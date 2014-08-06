using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Customers
{
    /// <filename>DeleteCustomerViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère la fenêtre de suppression de clients</summary>
    class DeleteCustomerViewModel : BaseViewModel
    {

        public Contact contact { get; set; }
        public ICommand cmdArchive { get; set; }
        public ICommand cmdDelete { get; set; }
        public int nbrStudent { get; set; }
        public int nbrRequest { get; set; }

        public Action CloseActionDelete { get; set; }

        /// <summary>
        /// Initialise les valeurs à binder et lie la commande de suppression à l'action
        /// </summary>
        /// <param name="contact">Le contact à supprimer</param>
        public DeleteCustomerViewModel(Contact contact)
        {
            this.cmdDelete = new RelayCommand<Object>(actDeleteCustomer);
            this.contact = contact;
            this.nbrRequest = contact.requests.Count();
            this.nbrStudent = contact.students.Count();

        }

        /// <summary>
        ///  Suppression du client et de toutes ses dépendences
        /// </summary>
        /// <param name="o">-</param>
        public void actDeleteCustomer(Object o) {

            int nbrPhone = contact.phones.Count;
            int nbrEmail = contact.emails.Count;
           
            // Suppression des téléphone
            for (int i = 0; i < nbrPhone; i++)
            {
                db.phones.Remove(contact.phones.First());
            }

            // Suppression des emails
            for (int i = 0; i < nbrEmail; i++)
            {
                db.emails.Remove(contact.emails.First());
            }
            
            // Suppression des demandes
            for (int i = 0; i < nbrRequest; i++ )
            {
                int nbrEvent = contact.requests.First().events.Count;

                //Suppression des évènements
                for (int j = 0; j < nbrEvent; j++)
                    db.events.Remove(contact.requests.First().events.First());

                db.requests.Remove(contact.requests.First());
            }

            // Suppression des étudiants
            for (int i = 0; i < nbrStudent; i++ )
            {
                db.students.Remove(contact.students.First());
            }

            mediator.NotifyViewModel(Helper.Event.DELETE_CUSTOMER, contact);
            db.contacts.Remove(contact);
            db.SaveChanges();
            this.CloseActionDelete();
        }
    }
}
