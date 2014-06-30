using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel
{
    class Tab
    {
        public String header { get; set; }

        public UserControl content { get; set; }

        public Tab(String header, UserControl content)
        {
            this.header = header;
            this.content = content;
        }
    }

    class MainViewModel : BaseViewModel
    {

        public String login { get; set; }

        public String pass { get; set; }

        public String message { get; set; }

        public ObservableCollection<Tab> tabs { get; set; }

        public ICommand btnLogin { get; set; }

        public Action CloseAction { get; set; }

        public MainViewModel()
        {
            btnLogin = new RelayCommand<object>(actLogin);

            tabs = new ObservableCollection<Tab>();
            tabs.Add(new Tab("Request", new View.RequestsUCView()));
            tabs.Add(new Tab("Page", new View.UserControl2()));

        }

        private void actLogin(object arg)
        {

            // login admin@admin.com pass admin
            // login test@testcom pass test
            if (login.Length != 0 && pass.Length != 0)
            {
                int nbrUser = db.users.Where(u => u.email == login && u.password == pass).Count();

                if (nbrUser == 1)
                {
                    View.RequestsView requestsView = new View.RequestsView();
                    requestsView.Show();

                    CloseAction();
                }
                else
                {
                    message = "Login ou mot de passe incorrect";
                    NotifyPropertyChanged("message");
                }
            }
            else
            {
                message = "Login ou mot de passe vide";
                NotifyPropertyChanged("message");
            }    
        }
    }
}
