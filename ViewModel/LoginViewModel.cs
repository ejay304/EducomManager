using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        public String login { get; set; }

        public String pass { get; set; }

        public String message { get; set; }

        public bool loading { get; set; }

        public ICommand btnLogin { get; set; }

        public Action CloseAction { get; set; }

        public LoginViewModel()
        {
            btnLogin = new RelayCommand<object>(actLogin);
        }

        private void actLogin(object arg)
        {
            this.loading = true;
            NotifyPropertyChanged("loading");


            var worker = new BackgroundWorker();
            worker.DoWork += this.checkLogin;
            worker.RunWorkerCompleted += this.MyDoWorkCompleted;
            worker.RunWorkerAsync();
        }

        private void MyDoWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (loading)
            {
                mediator.openMainView();
                CloseAction();
            }
        }


        private void checkLogin(object sender, DoWorkEventArgs e)
        {
            // login admin@admin.com pass admin
            // login test@testcom pass test
            if (login.Length != 0 && pass.Length != 0)
            {

                user user = db.users.Where(u => u.email == login && u.password == pass).First();

                if (user != null)
                {
                    mediator.user = user;
                    return;
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

            this.loading = false;
            NotifyPropertyChanged("loading");
        }
    }
}
