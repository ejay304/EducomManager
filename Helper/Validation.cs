using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeEDUCOM.Helper
{
    /// <filename>TabName.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Structure représentant une validation d'un champ dans un formulaire</summary>
    class Validation
    {
        public string message { get; set; }

        public bool valid { get; set; }
    }
}
