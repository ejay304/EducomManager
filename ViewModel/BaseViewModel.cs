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

    public class BaseViewModel : INotifyPropertyChanged
    {
        public EducomDb db = EducomDb.getInstance();

        public MediatorViewModel mediator = MediatorViewModel.getInstance();

        public event PropertyChangedEventHandler PropertyChanged;

        public static Dictionary<String,Tab> tabs { get; set; }

        public void NotifyPropertyChanged(string nomPropriete)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(nomPropriete));
        }

        public virtual void Update(string eventName, object item)
        {

        } 
    }
}
