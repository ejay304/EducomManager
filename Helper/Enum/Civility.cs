using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeEDUCOM.Helper.Enum
{
    class Civility : EnumDb
    {
        public static readonly Civility m = new Civility("m", "M.");
        public static readonly Civility mme = new Civility("mme", "Mme.");
      
        public static List<Civility> list { get; set; }
            
        private Civility(String value, String name) : base(value,name) {
        }

        public static int indexByValue(String value)
        {
            for (int i = 0; i < Civility.list.Count; i++)
                if (String.Compare(Civility.list.ElementAt(i).getValue(), value) == 0)
                    return i;
            return -1;
        }

        static Civility()
        {
            list = new List<Civility>();
            list.Add(m);
            list.Add(mme);
        }
    }
}
