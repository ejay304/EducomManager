using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeEDUCOM.Helper
{

    /// <filename>Event.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe contenant la liste des événements possibles</summary>
    class Event
    {
        public const string ADD_CUSTOMER = "add_customer";
        public const string DELETE_CUSTOMER = "delete_customer"; 

        public const string ADD_STUDENT = "add_student";
        public const string DELETE_STUDENT = "delete_student"; 
            
        public const string ADD_ORGANISATION = "add_organisation"; 
        public const string DELETE_ORGANISATION = "delete_organisation";

        public const string ADD_PROGRAM = "add_program";
        public const string DELETE_PROGRAM = "delete_program";

        public const string ADD_CAMPUS= "add_campus";
        public const string DELETE_CAMPUS = "delete_campus";

        public const string ADD_REQUEST = "add_request";
        public const string DELETE_REQUEST = "delete_request";

        public const string ADD_PROPOSITION = "add_proposition";
        public const string DELETE_PROPOSITION = "delete_proposition";

        public const string ADD_INSCRIPTION = "add_inscription";

    }
}
