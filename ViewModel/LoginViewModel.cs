using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
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

        private static int SaltValueSize = 8;
            

        public LoginViewModel()
        {
            btnLogin = new RelayCommand<object>(actLogin);

            actLogin(new object());
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

            this.login = "test";
            this.pass = "test";

            if (login.Length != 0 && pass.Length != 0)
            {

                user user = db.users.Where(u => u.email == login).First();

                if (user != null)
                {
                    if (HashPassword(pass, user.salt) == user.password)
                    {
                        mediator.user = user;
                        return;
                    }
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

        private String HashPassword(String password, String salt) {
            SHA256Managed sha256 = new SHA256Managed();
       
            String hash = String.Empty;

            byte[] valueToHash = new byte[Encoding.ASCII.GetByteCount(salt) + Encoding.ASCII.GetByteCount(password)];

            // Crée un tableau de byte avec les chaines de caractère du sel et du mot de passe contactené
            valueToHash = Encoding.ASCII.GetBytes(salt + password);

            // Hache le tableau de byte
            byte[] hashValue = sha256.ComputeHash(valueToHash);

            //Concerti le tableau de byte hashé en String
            foreach (byte bit in hashValue)
            {
                hash += bit.ToString("x2");
            }

            return hash;
        }

        private static string GenerateSaltValue()
        {
            
                // Création d'un générateur de nombre aléatoire.

                Random random = new Random(unchecked((int)DateTime.Now.Ticks));
                String saltValueString = String.Empty;

                if (random != null)
                {
                    byte[] saltValue = new byte[SaltValueSize];

                    // Génère un tableau de byte aléatoire
                    random.NextBytes(saltValue);

                    // Converti les tableau de byte en string
                    foreach (byte bit in saltValue)
                    {
                        saltValueString += bit.ToString("x2");
                    }

                    return saltValueString;
                }

            return null;
        }
    }
}
