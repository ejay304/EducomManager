using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel
{
    class AddRequestViewModel : BaseViewModel
    {

        private RequestsViewModel parentViewModel;

        public string description { get; set; }

        public string state { get; set; }

        public ICommand cmdAdd { get; set; }

        public Action CloseActionFormAdd { get; set; }

        public AddRequestViewModel(RequestsViewModel parentViewModel)
        {
            this.parentViewModel = parentViewModel;
            this.cmdAdd = new RelayCommand<object>(actAdd);
        }
        public void actAdd(object obj)
        {
            request r = new request();
            r.description = this.description;
            r.state = "done";
            r.user = db.users.First();

            // Voir les possibilité avec EF de modif la list et automatiquement gere la BD

            // Ajoute dans la liste
            parentViewModel.requests.Add(r);
            parentViewModel.NotifyPropertyChanged("requests");

            // Enregistre dans la base
            db.requests.Add(r);
            db.SaveChanges();

            this.CloseActionFormAdd();
        }
    }
}
