using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeEDUCOM.Helper.Enum
{
    class Journey : EnumDb
    {
        public static readonly Journey holidays = new Journey("holidays", "Camp de vacance");
        public static readonly Journey abroad = new Journey("abroad", "Séjour linguistique");
        public static readonly Journey university = new Journey("university", "Université");

        public static List<Journey> list { get; set; }
            
        private Journey(String value, String name) : base(value,name) {
        }

        public static int indexByValue(String value)
        {
            for (int i = 0; i < Journey.list.Count; i++)
                if (String.Compare(Journey.list.ElementAt(i).getValue(), value) == 0)
                    return i;
            return -1;
        }

        static Journey()
        {
            list = new List<Journey>();
            list.Add(holidays);
            list.Add(abroad);
            list.Add(university);
        }
    }
}
