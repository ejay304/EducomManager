using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeEDUCOM.Helper.Enum
{
   public sealed class Kinship
    {
       
            private readonly String name;
            private readonly String value;

            public static readonly Kinship father = new Kinship("father", "Père");
            public static readonly Kinship mother = new Kinship("mother", "Mère");
            public static readonly Kinship uncle = new Kinship("uncle", "Oncle");
            public static List<Kinship> list { get; set; }
            static Kinship()
            {
                list = new List<Kinship>();
                list.Add(father);
                list.Add(mother);
                list.Add(uncle);
            }

            private Kinship(String value, String name)
            {
                this.name = name;
                this.value = value;
            }

            public override String ToString()
            {
                return name;
            }
            public String getValue()
            {
                return name;
            }

        }
}
