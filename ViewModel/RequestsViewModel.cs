using PrototypeEDUCOM.Model;
using PrototypeEDUCOM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel
{
    class RequestsViewModel : BaseViewModel
    {
        // Utiliser une ObservableCollection (Au lieu de ICollection), de cette manière, lors de la modif d'un item de la list, 
        // la vue est directement avertit et mis a jour (dans ICollection, il gere la liste global mais pas chaque item)

        // Peut-etre plus propre de mettre les NotifyPropertyChanged directement dans les set des propriété, a voir
        public ObservableCollection<Model.request> requests { get; set; }

        private request currentRequest { get; set; }

        public string description { get; set; }

        public string state { get; set; }

        public ICommand cmdViewDetail { get; set; }

        public ICommand cmdDelete { get; set; }

        public ICommand cmdFormAddRequest { get; set; }

        public ICommand cmdFormEditRequest { get; set; }

        public ICommand cmdEdit { get; set; }

        public Action CloseActionFormEdit { get; set; }

        public RequestsViewModel() : base()
        {
            this.requests = new ObservableCollection<request>(db.requests.ToList());
            this.cmdViewDetail = new RelayCommand<request>(actViewDetail);
            this.cmdDelete = new RelayCommand<request>(actDelete);
            this.cmdFormAddRequest = new RelayCommand<object>(actFormAddRequest);
            this.cmdFormEditRequest = new RelayCommand<request>(actFormEditRequest);
           
            this.cmdEdit = new RelayCommand<request>(actEdit);
        }

        private void actFormAddRequest(object obj)
        {
            AddRequestViewModel addRequestViewModel = new AddRequestViewModel(this);
            FormAddRequestView addRequestView = new FormAddRequestView();

            addRequestView.DataContext = addRequestViewModel;
            addRequestViewModel.CloseActionFormAdd = new Action(() => addRequestView.Close());

            addRequestView.Show(); 
        }

        private void actFormEditRequest(request request)
        {
            View.FormEditRequestView formEditRequestView = new View.FormEditRequestView();
            formEditRequestView.DataContext = this;
            this.CloseActionFormEdit = new Action(() => formEditRequestView.Close());

            this.currentRequest = request;
            this.description = request.comment;

            formEditRequestView.Show();
        }

        public void actViewDetail(request request)
        {
            View.ShowRequestView showRequestsView = new View.ShowRequestView(request);
            showRequestsView.Show();
        }

        public void actDelete(request request)
        {
            requests.Remove(request);
            db.requests.Remove(request);
            db.SaveChanges();
            NotifyPropertyChanged("requests");
        }

        public void actEdit(object obj)
        {
            this.currentRequest.comment = this.description;

            db.SaveChanges();

            this.CloseActionFormEdit();
        }
    }
}
