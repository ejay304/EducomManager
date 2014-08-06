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
    /// <filename>MainViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère la fenêtre d'authentification de l'application</summary>
    class LoginViewModel : BaseViewModel
    {
        public String login { get; set; }

        public String pass { get; set; }

        public String message { get; set; }

        public bool loading { get; set; }

        public ICommand btnLogin { get; set; }

        public Action CloseAction { get; set; }

        private static int SaltValueSize = 8;
           

        /// <summary>
        /// Lie le boutton de connexion à l'action.
        /// </summary>
        public LoginViewModel()
        {
            btnLogin = new RelayCommand<object>(actLogin);
        }

        /// <summary>
        /// Authentifie l'utilisateur
        /// </summary>
        /// <param name="arg"></param>
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

        /// <summary>
        /// Test les valeurs saise d'authentification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkLogin(object sender, DoWorkEventArgs e)
        {
            // login admin@admin.com pass admin
            // login test@testcom pass test

            if (login.Length != 0)
            {

                User[] users = db.users.Where(u => u.email == login).ToArray();


                if (users.Count() != 0)
                {
                    User user = users[0];
                    if (HashPassword(pass, user.salt) == user.password)
                    {
                        mediator.user = user;
                        return;
                    }
                    else
                    {
                        message = "Nom d'utilisateur ou mot de passe incorrect";
                        NotifyPropertyChanged("message");
                    }
                }       
                else
                {
                    message = "Nom d'utilisateur ou mot de passe incorrect";
                    NotifyPropertyChanged("message");
                }
            }
            else
            {
                message = "Nom d'utilisateur vide";
                NotifyPropertyChanged("message");
            }

            this.loading = false;
            NotifyPropertyChanged("loading");
        }

        /// <summary>
        /// Réalise un hashage SHA256 avec sel
        /// </summary>
        /// <param name="password">le mot de passe saisi</param>
        /// <param name="salt">le sel</param>
        /// <returns>Le hash</returns>
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

        /// <summary>
        ///  Génère un sel aléatoire
        /// </summary>
        /// <returns>La valeur du sel</returns>
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
