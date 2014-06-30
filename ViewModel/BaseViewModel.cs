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
    public class Tab
    {
        public String header { get; set; }

        public UserControl content { get; set; }

        public Tab(String header, UserControl content)
        {
            this.header = header;
            this.content = content;
        }
    }

    public class BaseViewModel : INotifyPropertyChanged
    {
        protected EducomDb db = new EducomDb();

        public event PropertyChangedEventHandler PropertyChanged;

        public static ObservableCollection<Tab> tabs { get; set; }

        public void NotifyPropertyChanged(string nomPropriete)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(nomPropriete));
        }
    }
}
