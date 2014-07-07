using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeEDUCOM.Helper.Enum
{
    class Gender : EnumDb
    {

        public static readonly Gender man = new Gender("man", "Homme");
        public static readonly Gender woman = new Gender("woman", "Femme");
        public static List<Gender> list { get; set; }
        private Gender(String value, String name) : base(value,name) {
        }

        public static int indexByValue(String value)
        {
            for (int i = 0; i < Gender.list.Count; i++)
                if (String.Compare(Gender.list.ElementAt(i).getValue(), value) == 0)
                    return i;
            return -1;
        }
        static Gender()
        {
            list = new List<Gender>();
            list.Add(man);
            list.Add(woman);
        }
    }
}
