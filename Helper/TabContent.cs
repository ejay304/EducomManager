using PrototypeEDUCOM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PrototypeEDUCOM.Helper
{
    /// <filename>TabContent.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Représente le contenu d'onglet avec son contexte</summary>
    public class TabContent
    {
        public BaseViewModel tabViewModel { get; set; }

        public UserControl tabUC { get; set; }

        /// <summary>
        /// Constructeur du TabContent
        /// </summary>
        /// <param name="tabViewModel">Le dataContexte de l'onglet</param>
        /// <param name="tabUC">le contenu de l'onglet</param>
        public TabContent(BaseViewModel tabViewModel, UserControl tabUC)
        {
            this.tabViewModel = tabViewModel;
            this.tabUC = tabUC;
        }
    }
}
