using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PrototypeEDUCOM.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected EducomDb db = new EducomDb();

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string nomPropriete)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(nomPropriete));
        }
    }
}
