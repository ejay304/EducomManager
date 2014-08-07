using PrototypeEDUCOM.Helper;
using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace PrototypeEDUCOM.ViewModel
{

    /// <filename>BaseViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe abstraite implémantant les methodescommunes à tous les ViewModel </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public EducomDb db = EducomDb.getInstance();

        public MediatorViewModel mediator = MediatorViewModel.getInstance();

        public event PropertyChangedEventHandler PropertyChanged;

        public static Dictionary<String,Tab> tabs { get; set; }

        /// <summary>
        /// Indique à la vue que la propriété a été saisie
        /// </summary>
        /// <param name="nomPropriete">Nom de la propriété</param>
        public void NotifyPropertyChanged(string nomPropriete)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(nomPropriete));
        }

        /// <summary>
        /// Fonction de mise à jour en cas de notification d'événement
        /// </summary>
        /// <param name="eventName">Le type d'événement</param>
        /// <param name="item">l'objet concerné par l'événement</param>
        public virtual void Update(string eventName, object item)
        {

        } 
    }
}
