using PrototypeEDUCOM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PrototypeEDUCOM.Helper
{
    public class TabContent
    {
        public BaseViewModel tabViewModel { get; set; }

        public UserControl tabUC { get; set; }

        public TabContent(BaseViewModel tabViewModel, UserControl tabUC)
        {
            this.tabViewModel = tabViewModel;
            this.tabUC = tabUC;
        }
    }
}
