using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeEDUCOM.Helper
{
    /// <filename>Dictionaries.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe static contenant une liste dictionnaires de mot-clé</summary>
    static class Dictionaries
    {
        public static Dictionary<string, string> countries { get; set; }
        public static Dictionary<string, string> civilities { get; set; }
        public static Dictionary<string, string> genders { get; set; }
        public static Dictionary<string, string> kinships { get; set; }
        public static Dictionary<string, string> journeys { get; set; }
        public static Dictionary<string, string> users { get; set; }
        public static Dictionary<string, string> languages { get; set; }

        /// <summary>
        /// Constructeur statique permettant d'initialiser la valeur des dictionnaires
        /// </summary>
        static Dictionaries() {

            countries = new Dictionary<string, string>();
            countries.Add("suisse", "Suisse");
            countries.Add("france", "France");
            countries.Add("italie", "Italie");
            countries.Add("usa", "Etat-Unis");

            languages = new Dictionary<string, string>();
            languages.Add("fr", "Français");
            languages.Add("it", "Italien");
            languages.Add("en", "Anglais");

            civilities = new Dictionary<string, string>();
            civilities.Add("m", "M.");
            civilities.Add("mme", "Mme.");

            genders = new Dictionary<string, string>();
            genders.Add("man", "Homme");
            genders.Add("woman", "Femme");

            kinships = new Dictionary<string, string>();
            kinships.Add("father", "Père");
            kinships.Add("mother", "Mère");
            kinships.Add("uncle", "Oncle");

            journeys = new Dictionary<string, string>();
            journeys.Add("holidays", "Camp de vacance");
            journeys.Add("abroad", "Séjour linguistique");
            journeys.Add("university", "Université");

            users = new Dictionary<string, string>();
            users.Add("adviser", "Conseiller");
            users.Add("assistant", "Assistant");
            users.Add("administrator", "Administrateur");

        }
    }
}
