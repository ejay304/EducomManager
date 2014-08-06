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
    /// <filename>EditRequestViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère la fenêtre de modification d'une requête</summary>
    class EditRequestViewModel : BaseViewModel
    {
        public Request request { get; set; }
        public Student student { get; set; }
        public string journey { get; set; }
        public Dictionary<string, string> journeys { get { return Dictionaries.journeys; } set { } }

        public ICommand cmdEdit { get; set; }
        public Action CloseActionEdit { get; set; }

        /// <summary>
        /// Initialise les valeurs à binder et lie la commande d'édition à l'action
        /// </summary>
        /// <param name="request">La requête à modifier</param>
        public EditRequestViewModel(Request request)
        {
            this.request = request;
            this.journey = request.journey_type;
            this.student = request.student;
        
            this.cmdEdit = new RelayCommand<object>(actEdit);
        }

        /// <summary>
        /// Modifie la demande avec les valeurs saisie dans le formulaire
        /// </summary>
        /// <param name="obj"></param>
        public void actEdit(object obj)
        {
            this.request.journey_type = journey;
            this.request.student = student;

            db.SaveChanges();

            this.CloseActionEdit();  
        }
    }
}
