using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeEDUCOM.Helper.Enum
{
   public class Kinship : EnumDb
    {

        public static readonly Kinship father = new Kinship("father", "Père");
        public static readonly Kinship mother = new Kinship("mother", "Mère");
        public static readonly Kinship uncle = new Kinship("uncle", "Oncle");

        public static List<Kinship> list { get; set; }
            
        private Kinship(String value, String name) : base(value,name) {
        }

        public static int indexByValue(String value)
        {
            for (int i = 0; i < Kinship.list.Count; i++)
                if (String.Compare(Kinship.list.ElementAt(i).getValue(), value) == 0)
                    return i;
            return -1;
        }

        static Kinship()
        {
            list = new List<Kinship>();
            list.Add(father);
            list.Add(mother);
            list.Add(uncle);
        }
    }
}
