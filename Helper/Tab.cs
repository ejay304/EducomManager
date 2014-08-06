using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PrototypeEDUCOM.Helper
{
    /// <filename>Tab.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Représente un onglet affiché dans un TabControl</summary>
    public class Tab
    {
        public String header { get; set; }

        public UserControl content { get; set; }

        public String icon { get; set; }

        public Object entity { get; set; }

        public Boolean main { get; set; }
        /// <summary>
        /// Constructeur de Tab
        /// </summary>
        /// <param name="header">Le texte affiché dans l'entête de l'onglet</param>
        /// <param name="content">Le UserControl contenant le contenu à afficher</param>
        /// <param name="entity">L'entité affichée dans l'onglet</param>
        /// <param name="icon">Le liens vers l'icone de l'onglet (facultatif)</param>
        /// <param name="main">Indique si il s'agit d'une liste</param>
        public Tab(String header, UserControl content, Object entity, String icon, Boolean main = false)
        {
            this.header = header;
            this.content = content;
            this.entity = entity;
            this.icon = icon;
            this.main = main;
        }
    }
}
